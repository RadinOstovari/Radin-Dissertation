using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;


namespace MeshMorpher
{
	public class MorphController : MonoBehaviour
	{
		// compute shader needs to be in resources
		private const string ComputeShaderName = "MeshMorphComputeShader";

		private const string ShaderKernel = "CalculateVertices";

		private static readonly int MorphFloatShaderProperty = Shader.PropertyToID("morph");
		private static readonly int VerticesModeShaderProperty = Shader.PropertyToID("verticesMode");
		private static readonly int VertexBufferShaderProperty = Shader.PropertyToID("vertexBuffer");
		private static readonly int MorphToVertexBufferShaderProperty = Shader.PropertyToID("morphToVertexBuffer");


		[SerializeField]
		private MeshFilter meshFilter;

		[SerializeField]
		private Mesh morphFromMesh;

		[SerializeField]
		private Mesh morphToMesh;


		[Header("Morph parameters")]
		[SerializeField]
		[Range(0, 1)]
		private float morph = 0.0f;

		[SerializeField]
		private VerticesMorphMode verticesMorphMode = VerticesMorphMode.Parallel;

		[SerializeField]
		[Tooltip("If toggle is enabled then vertices are created in transform.position instead of previous vertices positions")]
		private bool useTransformPosition = false;

		[Space(3)]
		[SerializeField]
		[Tooltip("Prevents reshuffling of triangles but calculation is much longer")]
		public bool keepTrianglesOrder = false;

		private TrackableValue currentAmountOfVertices = new TrackableValue(0);

		private TrianglesProvider trianglesProviderFrom;
		private TrianglesProvider trianglesProviderTo;
		private float finalMorph = 20f;


		// temporary values used for calculation - to prevent extra memory allocation
		private List<int> trianglesCalculated = new List<int>();
		private List<Vector2> uvsCalculated = new List<Vector2>();

		private Vector3[] newVerticesCalculated = null;

		private List<Vector3> verticesFromCalculated = new List<Vector3>();
		private List<Vector3> verticesToCalculated = new List<Vector3>();


		private ComputeShader computeShader;
		private ComputeShader ComputeShader
		{
			get
			{
				if (computeShader == null)
				{
					computeShader = (ComputeShader)Resources.Load(ComputeShaderName);
				}
				return computeShader;
			}
			set
			{
				computeShader = value;
			}
		}

		private void Awake()
		{
			int mymorphValue = PlayerPrefs.GetInt("MyMorph");
			finalMorph = (float)mymorphValue;
			UpdateMorphController();
		}


		public void UpdateMeshes()
		{
			if (meshFilter == null || morphFromMesh == null || morphToMesh == null)
			{
				Debug.LogWarning("Not all meshes in morph controller are set");
				return;
			}

			if (morphFromMesh != meshFilter.sharedMesh && morph == 0)
			{
				meshFilter.sharedMesh = Instantiate(morphFromMesh);
			}

			meshFilter.mesh = Instantiate(meshFilter.sharedMesh);

			if (keepTrianglesOrder)
			{
				trianglesProviderFrom = new OrderedTrianglesProvider(morphFromMesh);
				trianglesProviderTo = new OrderedTrianglesProvider(morphToMesh);
			}
			else
			{
				trianglesProviderFrom = new UnorderedTrianglesProvider(morphFromMesh);
				trianglesProviderTo = new UnorderedTrianglesProvider(morphToMesh);
			}

			currentAmountOfVertices.SetCurrentValue(meshFilter.sharedMesh.vertexCount);

			verticesFromCalculated = morphFromMesh.vertices.ToList();
			verticesToCalculated = morphToMesh.vertices.ToList();
		}

		/// <summary>
		/// Recalculate and set up everything (triangles, vertices, uvs, meshes etc.)
		/// </summary>
		public void UpdateMorphController()
		{
			UpdateMeshes();
			//MeshMorph();
			StartCoroutine(SetMeshMorphValue(1.0f, finalMorph));
		}

		/// <summary>
		/// Use it to setup everything required for morph controller from script
		/// </summary>
		/// <param name="newMeshFilter"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public void SetMorphController(MeshFilter newMeshFilter, Mesh from, Mesh to)
		{
			meshFilter = newMeshFilter;
			morphFromMesh = from;
			morphToMesh = to;

			UpdateMorphController();
		}


		public void SetMeshesToMorph(Mesh from, Mesh to)
		{
			morphFromMesh = from;
			morphToMesh = to;

			UpdateMorphController();
		}

		public void SetMeshToMorphFrom(Mesh from)
		{
			morphFromMesh = from;

			UpdateMorphController();
		}

		public void SetMeshToMorphTo(Mesh toMesh)
		{
			morphToMesh = toMesh;

			UpdateMorphController();
		}

		/// <summary>
		/// Setting mesh morph value and triggers update of mesh filter
		/// </summary>
		/// <param name="newValue">New value of the mesh morph</param>
		public void SetMeshMorphValue(float newValue)
		{
			if (morph != newValue)
			{
				morph = newValue;
				MeshMorph();
			}
		}

		/// <summary>
		/// Setting mesh morph value and triggers update of mesh filter in time used as coroutine
		/// </summary>
		/// <param name="newValue"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public IEnumerator SetMeshMorphValue(float newValue, float time)
		{
			yield return new WaitForSeconds(10);
			float startValue = morph;
			float curTime = 0.0f;
			while (curTime <= time)
			{
				curTime += Time.deltaTime;
				SetMeshMorphValue(Mathf.Lerp(startValue, newValue, curTime / time));
				yield return null;
			}
			yield return null;
		}

		/// <summary>
		/// Main method responsible for vertices and triangles calculation using defined morph mode and value
		/// </summary>
		private void MeshMorph()
		{
			Mesh mesh = new Mesh();
			float morphValue = morph;
			if (morphValue != 0 && morphValue != 1)
			{
				// calculate proper amount of vertices
				int differenceInVertices = morphToMesh.vertexCount - morphFromMesh.vertexCount;
				int newAmountOfVertices = morphFromMesh.vertexCount +
					Mathf.FloorToInt(differenceInVertices * morphValue);

				newVerticesCalculated = new Vector3[newAmountOfVertices];
				CalculateVerticesPosition(newVerticesCalculated, newAmountOfVertices);
				mesh.vertices = newVerticesCalculated;


				// if amount of vertices didnt change we do not need to provide new triangles and uvs
				if (currentAmountOfVertices.SetCurrentValue(newAmountOfVertices))
				{
					// triangles
					trianglesCalculated.Clear();
					CalculateProperTriangles(trianglesCalculated);
					mesh.SetTriangles(trianglesCalculated, 0);

					// uvs
					uvsCalculated.Clear();
					CalculateUVs(uvsCalculated, newAmountOfVertices, morphToMesh.vertexCount);
					mesh.SetUVs(0, uvsCalculated);
				}
				else
				{
					mesh.triangles = meshFilter.sharedMesh.triangles;
					mesh.uv = meshFilter.sharedMesh.uv;
				}

				// mesh.RecalculateBounds(); not neeeded because 
				// 'Assigning triangles automatically recalculates the bounding volume' from documentation
				mesh.RecalculateNormals();

				mesh.name = "morphedMesh";
			}
			else
			{
				mesh = morphValue == 1 ? morphToMesh : morphFromMesh;
			}


			mesh.UploadMeshData(false);
			meshFilter.mesh = mesh;
			meshFilter.sharedMesh = mesh;
		}

		private void CalculateProperTriangles(List<int> triangles)
		{
			int index1 = Mathf.FloorToInt(Mathf.Lerp(morphFromMesh.vertexCount - 1, 0, morph));
			triangles.AddRange(trianglesProviderFrom.GetTriangles(index1));

			int index2 = Mathf.FloorToInt(Mathf.Lerp(0, morphToMesh.vertexCount - 1, morph));
			triangles.AddRange(trianglesProviderTo.GetTriangles(index2));
		}

		private void CalculateUVs(List<Vector2> uvs, int amountOfVertices, int meshToVertexAmount)
		{
			var uvsFrom = morphFromMesh.uv;
			var uvsTo = morphToMesh.uv;

			for (int i = 0; i < Mathf.Min(amountOfVertices, meshToVertexAmount); i++)
				uvs.Add(uvsTo[i]);

			// we still need to add rest
			if (meshToVertexAmount < amountOfVertices)
			{
				for (int i = 0; i < amountOfVertices - meshToVertexAmount; i++)
					uvs.Add(uvsFrom[i]);
			}
		}

		/// <summary>
		/// Use compute shader to calculate correct positions of the vertices in the mesh
		/// </summary>
		private void CalculateVerticesPosition(Vector3[] newVertices, int newAmountOfVertices)
		{

			ComputeShader.SetFloat(MorphFloatShaderProperty, morph);
			ComputeShader.SetInt(VerticesModeShaderProperty, (int)verticesMorphMode);

			int kernel = ComputeShader.FindKernel(ShaderKernel);
			int stride = 12; // 3 floats

			var vertexBuffer = CreateComputeShaderBuffer(kernel, VertexBufferShaderProperty,
				FillListWithMissingValues(verticesFromCalculated, newAmountOfVertices), stride);
			var morphToVertexBuffer = CreateComputeShaderBuffer(kernel, MorphToVertexBufferShaderProperty,
				FillListWithMissingValues(verticesToCalculated, newAmountOfVertices), stride);

			ComputeShader.Dispatch(kernel, newAmountOfVertices, 1, 1);

			vertexBuffer.GetData(newVertices);

			vertexBuffer.Dispose();
			morphToVertexBuffer.Dispose();
		}

		private ComputeBuffer CreateComputeShaderBuffer(int kernel, int bufferId, List<Vector3> list, int stride)
		{
			ComputeBuffer buffer = new ComputeBuffer(list.Count, stride);
			buffer.SetData(list);
			ComputeShader.SetBuffer(kernel, bufferId, buffer);
			return buffer;
		}

		private List<Vector3> FillListWithMissingValues(List<Vector3> vertices, int newVerticesAmount)
		{
			// we do not need to fill dummy values
			if (vertices.Count > newVerticesAmount) return vertices.GetRange(0, newVerticesAmount);

			int count = vertices.Count;
			for (int i = 0; i < newVerticesAmount - count; i++)
			{
				vertices.Add(useTransformPosition ? transform.position : vertices[i]); // repeating values from the beginning
			}

			return vertices;
		}

		private enum VerticesMorphMode
		{
			Parallel,
			Mix,
			OneByOne
		}


		#region InspectorDebugData

		public string GetModelsData()
		{
			StringBuilder sb = new StringBuilder();

			if (meshFilter == null)
				return "Mesh Filter is null";

			sb.AppendLine(GetMeshData("Current Mesh", meshFilter.sharedMesh));
			sb.AppendLine(GetMeshData($"Model 1 ({GetMeshNameOrNull(morphFromMesh)})", morphFromMesh));
			sb.AppendLine(GetMeshData($"Model 2 ({GetMeshNameOrNull(morphToMesh)})", morphToMesh));

			return sb.ToString();
		}

		private string GetMeshNameOrNull(Mesh mesh)
		{
			return mesh == null ? "NULL" : mesh.name;
		}

		private string GetMeshData(string id, Mesh mesh)
		{
			return mesh == null ? $"{id} [NULL]" :
				$"{id} [ Vertices:{mesh.vertexCount} Triangles:{mesh.triangles.Length}]";
		}

		#endregion
	}
}
