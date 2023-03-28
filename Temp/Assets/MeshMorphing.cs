using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMorphing : MonoBehaviour
{
    public Mesh mesh1;
    public Mesh mesh2;
    public float blendAmount = 0.5f;
    public bool updateAutomatically = true;

    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found");
        }
        else if (mesh1 == null || mesh2 == null)
        {
            Debug.LogError("Meshes not assigned");
        }
        else
        {
            if (updateAutomatically)
            {
                UpdateMesh();
            }
        }
    }

    void Update()
    {
        if (updateAutomatically)
        {
            UpdateMesh();
        }
    }

    public void UpdateMesh()
    {
        CombineInstance[] combineInstances = new CombineInstance[2];

        combineInstances[0].mesh = mesh1;
        combineInstances[0].transform = transform.localToWorldMatrix;

        combineInstances[1].mesh = mesh2;
        combineInstances[1].transform = transform.localToWorldMatrix;

        meshFilter.mesh = new Mesh();
        
        // Check if the method overload with 4 parameters exists
        System.Type[] combineTypes = new System.Type[] { typeof(bool), typeof(bool), typeof(bool) };
        System.Reflection.MethodInfo combineMethod = typeof(Mesh).GetMethod("CombineMeshes", combineTypes);
        if (combineMethod != null)
        {
            // Unity version 2019.1 and later
            object[] combineParams = new object[] { combineInstances, true, true, false };
            combineMethod.Invoke(meshFilter.mesh, combineParams);
        }
        else
        {
            // Unity version prior to 2019.1
            meshFilter.mesh.CombineMeshes(combineInstances, true, true);
        }

        meshFilter.mesh.RecalculateNormals();

        for (int i = 0; i < meshFilter.mesh.vertices.Length; i++)
        {
            meshFilter.mesh.vertices[i] = Vector3.Lerp(mesh1.vertices[i], mesh2.vertices[i], blendAmount);
        }

        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}



