using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    void Start()
    {
        Light myLight = GetComponent<Light>();
        myLight.enabled = false;
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        Light myLight = GetComponent<Light>();

        yield return new WaitForSeconds(10f); // wait for 5 seconds

        while (true)
        {
            myLight.enabled = true;
            yield return new WaitForSeconds(0.1f);
            myLight.enabled = false;
            break;
        }
    }
}

