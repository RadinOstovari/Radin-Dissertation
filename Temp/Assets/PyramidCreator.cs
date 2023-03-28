using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidCreator : MonoBehaviour
{
    public float baseWidth = 2f;    // The width of the base of the pyramid
    public float height = 2f;       // The height of the pyramid
    public Material material;       // The material to apply to the pyramid

    void Start()
    {
        // Create a new mesh and game object for the pyramid
        Mesh pyramidMesh = new Mesh();
        GameObject pyramidObject = new GameObject("Pyramid");

        // Set the pyramid's mesh and material
        pyramidObject.AddComponent<MeshFilter>().mesh = pyramidMesh;
        pyramidObject.AddComponent<MeshRenderer>().material = material;

        // Calculate the vertices and triangles of the pyramid's mesh
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, height, 0f),   // Apex of pyramid
            new Vector3(baseWidth / 2f, 0f, baseWidth / 2f),    // Bottom right corner
            new Vector3(baseWidth / 2f, 0f, -baseWidth / 2f),   // Bottom left corner
            new Vector3(-baseWidth / 2f, 0f, -baseWidth / 2f),  // Top left corner
            new Vector3(-baseWidth / 2f, 0f, baseWidth / 2f)    // Top right corner
        };
        int[] triangles = new int[]
        {
            0, 1, 2,   // Front face
            0, 2, 3,   // Left face
            0, 3, 4,   // Back face
            0, 4, 1,   // Right face
            1, 2, 3, 4 // Base of pyramid
        };

        // Apply the vertices and triangles to the mesh
        pyramidMesh.vertices = vertices;
        pyramidMesh.triangles = triangles;

        // Recalculate the normals of the mesh
        pyramidMesh.RecalculateNormals();

        // Move the pyramid to the position of this game object
        pyramidObject.transform.position = transform.position;
    }
}

