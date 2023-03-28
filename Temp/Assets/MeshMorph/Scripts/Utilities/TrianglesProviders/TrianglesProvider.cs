using System;

using UnityEngine;

namespace MeshMorpher
{
	public abstract class TrianglesProvider
	{

		public TrianglesProvider(Mesh mesh)
		{
			Initialize(mesh);
		}

		protected abstract void Initialize(Mesh mesh);

		public abstract int[] GetTriangles(int index);

		protected class Triangle : IComparable<Triangle>
		{
			private readonly int index1;
			private readonly int index2;
			private readonly int index3;

			public int MaxIndex { get; private set; }

			private int[] triangle = new int[3];

			public Triangle(int i1, int i2, int i3)
			{
				index1 = i1;
				index2 = i2;
				index3 = i3;
				MaxIndex = Mathf.Max(i1, i2, i3);

				triangle = new int[] { i1, i2, i3 };
			}

			public int[] GetTriangle()
			{
				return triangle;
			}

			public override string ToString()
			{
				return $"Triangle {index1}, {index2}, {index3}";
			}

			public int CompareTo(Triangle other)
			{
				return MaxIndex - other.MaxIndex;
			}
		}
	}
}

