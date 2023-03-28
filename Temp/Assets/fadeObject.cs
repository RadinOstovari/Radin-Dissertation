using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeObject : MonoBehaviour
{
    public GameObject balloon = null;
    
    public void Start()
    {
        balloon.SetActive(false);
        StartCoroutine(WaitBeforeShow());
    }

    private IEnumerator WaitBeforeShow()
    {
        yield return new WaitForSeconds(5);
        balloon.SetActive(true);
    }
}
