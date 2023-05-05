using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class SceneChanger : MonoBehaviour
{
    //public int nextSceneIndex; // the index of the next scene to load
    //public string otherScriptName; // the name of the script with the array you want to check
    //ToggleArray ToggleArray;
    //int[] myArray;
    List<int> savedList = new List<int>();
    
    void Start()
    {
        //string savedJsonString = PlayerPrefs.GetString("MyList");
        string intString = PlayerPrefs.GetString("MyList");
        //List<int> originalList = JsonUtility.FromJson<List<int>>(savedJsonString);
        List<int> originalList = intString.Split(',').Select(int.Parse).ToList();
        savedList.AddRange(originalList);
        //int check = originalList.Count;
        //Debug.Log(check);
        /*string myArrayString = PlayerPrefs.GetString("MyArray");
        myArray = myArrayString.Split(',').Select(int.Parse).ToArray();*/
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
            int randomIndex = UnityEngine.Random.Range(0, savedList.Count);
            //Debug.Log(randomIndex);
            int nextSceneIndex = savedList[randomIndex];
            //Debug.Log(nextSceneIndex);
            savedList.RemoveAt(randomIndex);
            //string updatedJsonList = JsonUtility.ToJson(savedList);
            string updatedList = string.Join(",", savedList.Select(i => i.ToString()).ToArray());
            PlayerPrefs.SetString("MyList", updatedList);
            /*while (true)
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
            }*/
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

