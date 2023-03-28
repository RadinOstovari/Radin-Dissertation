using System.Collections.Generic;

using UnityEngine;

namespace MeshMorpher
{
	public class OrderedTrianglesProvider : TrianglesProvider
	{
		private readonly Dictionary<int, List<Triangle>> trianglesProvider = new Dictionary<int, List<Triangle>>();

		public OrderedTrianglesProvider(Mesh mesh) : base(mesh) { }

		protected override void Initialize(Mesh mesh)
		{
			int[] triangles = mesh.triangles;

			if (triangles.Length % 3 != 0)
			{
				Debug.LogError($"The amount of triangles is not divided by 3 for mesh {mesh.name}");
				return;
			}

			for (int i = 0; i < triangles.Length; i += 3)
			{
				Triangle newTriangle = new Triangle(triangles[i], triangles[i + 1], triangles[i + 2]);
				if (!trianglesProvider.ContainsKey(newTriangle.MaxIndex))
				{
					trianglesProvider.Add(newTriangle.MaxIndex, new List<Triangle>());
				}
				trianglesProvider[newTriangle.MaxIndex].Add(newTriangle);
			}
		}

		public override int[] GetTriangles(int index)
		{
			List<int> triangles = new List<int>();
			for (int i = 0; i <= index; i++)
			{
				if (trianglesProvider.ContainsKey(i))
				{
					var trianglesToAdd = trianglesProvider[i];
					foreach (var triangle in trianglesToAdd)
					{
						triangles.AddRange(triangle.GetTriangle());
					}
				}
			}
			return triangles.ToArray();
		}

		
	}
}

