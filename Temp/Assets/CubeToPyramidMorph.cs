using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeToPyramidMorph : MonoBehaviour
{
    public float morphTime = 10f;       // The time it takes to complete the morph
    public Material material;           // The material to apply to the morphed object

    private Mesh mesh;
    private Vector3[] vertices;
    private float timer = 0f;

    void Start()
    {
        // Get the mesh of the object and make a copy of its vertices
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // Set the material of the object
        GetComponent<MeshRenderer>().material = material;
    }

    void Update()
    {
        // Increment the timer by the time that has passed since the last frame
        timer += Time.deltaTime;

        // Calculate how much of the morph has been completed so far
        float morphPercentage = Mathf.Clamp01(timer / morphTime);

        // Pull the vertices of the mesh to create the pyramid shape
        for (int i = 0; i < vertices.Length; i++)
        {
            // Calculate the new position of the vertex based on the morph percentage
            Vector3 vertexPosition = vertices[i];
            if (vertexPosition.y == 0f)
            {
                // Bottom vertices are moved to the center to form the apex
                vertexPosition = Vector3.Lerp(vertexPosition, Vector3.zero, morphPercentage);
            }
            else
            {
                // Top vertices are moved away from the center to form the base
                vertexPosition = Vector3.Lerp(vertexPosition, new Vector3(vertexPosition.x, 0f, vertexPosition.z), morphPercentage);
            }

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

