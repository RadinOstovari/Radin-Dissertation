using System;
using System.Collections.Generic;

using UnityEngine;

namespace MeshMorpher
{
	public class UnorderedTrianglesProvider : TrianglesProvider
	{
		private int[] triangles;

		private int[] whereToSliceTriangles;

		public UnorderedTrianglesProvider(Mesh mesh) : base(mesh) { }

		protected override void Initialize(Mesh mesh)
		{
			var trianglesFromMesh = mesh.triangles;
			if (trianglesFromMesh.Length % 3 != 0)
			{
				Debug.LogError($"The amount of triangles is not divided by 3 for mesh {mesh.name}");
				return;
			}

			List<Triangle> trianglesList = new List<Triangle>();

			for (int i = 0; i < trianglesFromMesh.Length; i += 3)
				trianglesList.Add(new Triangle(trianglesFromMesh[i], trianglesFromMesh[i + 1], trianglesFromMesh[i + 2]));

			trianglesList.Sort();
			List<int> trianglesIntList = new List<int>();
			foreach (var t in trianglesList)
				trianglesIntList.AddRange(t.GetTriangle());

			triangles = trianglesIntList.ToArray();

			whereToSliceTriangles = new int[triangles.Length];

			int tIndex = 0;
			int whereToSlice = 1;
			while (whereToSlice < whereToSliceTriangles.Length)
			{
				while (tIndex < triangles.Length && Math.Max(triangles[tIndex], Math.Max(triangles[tIndex + 1], triangles[tIndex + 2])) <= whereToSlice)
				{
					tIndex += 3;
					whereToSliceTriangles[whereToSlice] = tIndex;
				}

				if (whereToSliceTriangles[whereToSlice] == 0)
					whereToSliceTriangles[whereToSlice] = whereToSliceTriangles[whereToSlice - 1];

				whereToSlice++;

			}
		}

		public override int[] GetTriangles(int index)
		{
			int newAmount = whereToSliceTriangles[index];
			int[] trianglesCalculated = new int[newAmount];
			for (int i = 0; i < newAmount; i++)
				trianglesCalculated[i] = triangles[i];

			return trianglesCalculated;
		}
	}
}

