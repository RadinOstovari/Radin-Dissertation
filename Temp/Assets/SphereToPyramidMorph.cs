using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToPyramidMorph : MonoBehaviour
{
    public GameObject sphere;
    public GameObject pyramid;
    public float morphTime = 40f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sphere.SetActive(true);
        pyramid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float morphPercentage = Mathf.Clamp01(timer / morphTime);

        // Morph from sphere to pyramid
        sphere.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, morphPercentage);
        pyramid.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, morphPercentage);

        // Switch the active object when the morph is complete
        if (timer >= morphTime)
        {
            sphere.SetActive(false);
            pyramid.SetActive(true);
        }
    }
}

