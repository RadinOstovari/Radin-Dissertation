using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeToSphereMorph : MonoBehaviour
{
    public float morphTime = 10f;       // The time it takes to complete the morph
    public Material material;           // The material to apply to the morphed object

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] originalVertices;
    private float timer = 0f;

    void Start()
    {
        // Get the mesh of the object and make a copy of its vertices
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        vertices = new Vector3[originalVertices.Length];
        originalVertices.CopyTo(vertices, 0);

        // Set the material of the object
        GetComponent<MeshRenderer>().material = material;
    }

    void Update()
    {
        // Increment the timer by the time that has passed since the last frame
        timer += Time.deltaTime;

        // Calculate how much of the morph has been completed so far
        float morphPercentage = Mathf.Clamp01(timer / morphTime);

        // Smooth out the edges of the mesh to create the sphere shape
        for (int i = 0; i < vertices.Length; i++)
        {
            // Calculate the new position of the vertex based on the morph percentage
            Vector3 vertexPosition = vertices[i];
            Vector3 originalPosition = originalVertices[i];
            vertexPosition = Vector3.Lerp(vertexPosition, originalPosition.normalized, morphPercentage);

            // Update the vertex position in the array
            vertices[i] = vertexPosition;
        }

        // Apply the modified vertices to the mesh
        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        // If the morph is complete, stop updating the mesh
        if (timer >= morphTime)
        {
            enabled = false;
        }
    }
}

