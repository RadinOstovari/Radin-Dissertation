using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class SceneChanger : MonoBehaviour
{
    public int nextSceneIndex; // the index of the next scene to load
    //public string otherScriptName; // the name of the script with the array you want to check
    //ToggleArray ToggleArray;
    int[] myArray;
    
    void Start()
    {
        string myArrayString = PlayerPrefs.GetString("MyArray");
        myArray = myArrayString.Split(',').Select(int.Parse).ToArray();
        /*GameObject otherObject = GameObject.Find("VR Mask Quad 1");

        // Get a reference to the script on the GameObject
        ToggleArray = otherObject.GetComponent<ToggleArray>();*/

        // Check if the integer is in the array
       //int index = Array.IndexOf(ToggleArray.myArray, nextSceneIndex);
        //Debug.Log("Index of " + nextSceneIndex + " is " + index);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            while (true)
            {
                int index = Array.IndexOf(myArray, nextSceneIndex);
                if(index == -1)
                {
                    nextSceneIndex++;
                }
                else
                {
                    //SceneManager.LoadScene(nextSceneIndex);
                    break;
                }
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

