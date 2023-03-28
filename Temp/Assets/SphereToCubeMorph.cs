using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToCubeMorph : MonoBehaviour
{
    public GameObject sphere;
    public GameObject cube;
    public float morphTime = 10f;
    private float timer = 0f;

    void Start()
    {
        // Set the initial state of the objects
        sphere.SetActive(true);
        cube.SetActive(false);
    }

    void Update()
    {
        // Increment the timer by the time that has passed since the last frame
        timer += Time.deltaTime;

        // Calculate how much of the morph has been completed so far
        float morphPercentage = Mathf.Clamp01(timer / morphTime);

        // Morph the sphere into the cube using a Lerp function
        sphere.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, morphPercentage);
        cube.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, morphPercentage);

        // If the morph is complete, switch the active object and reset the timer
        if (timer >= morphTime)
        {
            sphere.SetActive(false);
            cube.SetActive(true);
            timer = 0f;
        }
    }
}

