using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using TMPro;

public class ToggleArray : MonoBehaviour
{
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;
    public Toggle toggle5;
    public Toggle toggle6;
    public Toggle toggle7;
    public Toggle toggle8;
    //public int nextSceneIndex;
    public TMP_InputField inputField;
    //private int index;

    //public int[] myArray;
    List<int> scenesList = new List<int>();

    void Start()
    {
        /*myArray = new int[16];
        myArray[0] = 0;
        myArray[1] = 0;
        myArray[2] = 0;
        myArray[3] = 0;
        myArray[4] = 0;
        myArray[5] = 0;
        myArray[6] = 0;
        myArray[7] = 0;
        myArray[8] = 0;
        myArray[9] = 0;
        myArray[10] = 0;
        myArray[11] = 0;
        myArray[12] = 0;
        myArray[13] = 0;
        myArray[14] = 0;
        myArray[15] = 0;*/

        // Add listeners to toggles
        /*toggle1.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle2.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle3.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle4.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle5.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle6.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle7.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });
        toggle8.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });*/
    }

    /*void OnToggleValueChanged()
    {
        if (toggle1.isOn)
        {
            myArray[0] = 1;
            myArray[1] = 2;
            scenesList.Add(1);
            scenesList.Add(2);
        }
        else if (!toggle1.isOn)
        {
            myArray[0] = 0;
            myArray[1] = 0;
        }
        if (toggle2.isOn)
        {
            myArray[2] = 3;
            myArray[3] = 4;
            scenesList.Add(3);
            scenesList.Add(4);
        }
        else if (!toggle2.isOn)
        {
            myArray[2] = 0;
            myArray[3] = 0;
        }
        if (toggle3.isOn)
        {
            myArray[4] = 5;
            myArray[5] = 6;
            scenesList.Add(5);
            scenesList.Add(6);
        }
        else if (!toggle3.isOn)
        {
            myArray[4] = 0;
            myArray[5] = 0;
        }
        if (toggle4.isOn)
        {
            myArray[6] = 7;
            myArray[7] = 8;
            scenesList.Add(7);
            scenesList.Add(8);
        }
        else if (!toggle4.isOn)
        {
            myArray[6] = 0;
            myArray[7] = 0;
        }
        if (toggle5.isOn)
        {
            myArray[8] = 9;
            myArray[9] = 10;
            scenesList.Add(9);
            scenesList.Add(10);
        }
        else if (!toggle5.isOn)
        {
            myArray[8] = 0;
            myArray[9] = 0;
        }
        if (toggle6.isOn)
        {
            myArray[10] = 11;
            myArray[11] = 12;
            scenesList.Add(11);
            scenesList.Add(12);
        }
        else if (!toggle6.isOn)
        {
            myArray[10] = 0;
            myArray[11] = 0;
        }
        if (toggle7.isOn)
        {
            myArray[12] = 13;
            myArray[13] = 14;
            scenesList.Add(13);
            scenesList.Add(14);
        }
        else if (!toggle7.isOn)
        {
            myArray[12] = 0;
            myArray[13] = 0;
        }
        if (toggle8.isOn)
        {
            myArray[14] = 15;
            myArray[15] = 16;
            scenesList.Add(15);
            scenesList.Add(16);
        }
        else if (!toggle8.isOn)
        {
            myArray[14] = 0;
            myArray[15] = 0;
        }
    }*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (toggle1.isOn)
            {
                //myArray[0] = 1;
                //myArray[1] = 2;
                scenesList.Add(1);
                scenesList.Add(2);
            }
            /*else if (!toggle1.isOn)
            {
                myArray[0] = 0;
                myArray[1] = 0;
            }*/
            if (toggle2.isOn)
            {
                //myArray[2] = 3;
                //myArray[3] = 4;
                scenesList.Add(3);
                scenesList.Add(4);
            }
            /*else if (!toggle2.isOn)
            {
                myArray[2] = 0;
                myArray[3] = 0;
            }*/
            if (toggle3.isOn)
            {
                //myArray[4] = 5;
                //myArray[5] = 6;
                scenesList.Add(5);
                scenesList.Add(6);
            }
            /*else if (!toggle3.isOn)
            {
                myArray[4] = 0;
                myArray[5] = 0;
            }*/
            if (toggle4.isOn)
            {
                //myArray[6] = 7;
                //myArray[7] = 8;
                scenesList.Add(7);
                scenesList.Add(8);
            }
            /*else if (!toggle4.isOn)
            {
                myArray[6] = 0;
                myArray[7] = 0;
            }*/
            if (toggle5.isOn)
            {
                //myArray[8] = 9;
                //myArray[9] = 10;
                scenesList.Add(9);
                scenesList.Add(10);
            }
            /*else if (!toggle5.isOn)
            {
                myArray[8] = 0;
                myArray[9] = 0;
            }*/
            if (toggle6.isOn)
            {
                //myArray[10] = 11;
                //myArray[11] = 12;
                scenesList.Add(11);
                scenesList.Add(12);
            }
            /*else if (!toggle6.isOn)
            {
                myArray[10] = 0;
                myArray[11] = 0;
            }*/
            if (toggle7.isOn)
            {
                //myArray[12] = 13;
                //myArray[13] = 14;
                scenesList.Add(13);
                scenesList.Add(14);
            }
            /*else if (!toggle7.isOn)
            {
                myArray[12] = 0;
                myArray[13] = 0;
            }*/
            if (toggle8.isOn)
            {
                //myArray[14] = 15;
                //myArray[15] = 16;
                scenesList.Add(15);
                scenesList.Add(16);
            }
            /*else if (!toggle8.isOn)
            {
                myArray[14] = 0;
                myArray[15] = 0;
            }*/
            //int index = Array.IndexOf(myArray, nextSceneIndex);
            //Debug.Log(index);
            /*while (true)
            {
                //Debug.Log(nextSceneIndex);
                //int index = Array.IndexOf(myArray, nextSceneIndex);
                int index = scenesList.IndexOf(nextSceneIndex);
                if(index == -1)
                {
                    nextSceneIndex++;
                }
                else
                {
                    //SceneManager.LoadScene(nextSceneIndex);
                    scenesList.RemoveAt(index);
                    break;
                }
            }*/
            int randomIndex = UnityEngine.Random.Range(0, scenesList.Count);
            int nextSceneIndex = scenesList[randomIndex];
            scenesList.RemoveAt(randomIndex);
            int result = 0;
            int.TryParse(inputField.text, out result);
            PlayerPrefs.SetInt("MyInt", result);
            /*string myArrayString = string.Join(",", myArray.Select(i => i.ToString()).ToArray());
            PlayerPrefs.SetString("MyArray", myArrayString);*/
            //string jsonString = JsonUtility.ToJson(scenesList);
            string intString = string.Join(",", scenesList.Select(i => i.ToString()).ToArray());
            //Debug.Log(intString);
            PlayerPrefs.SetString("MyList", intString);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    /*public int[] GetArray()
    {
        return myArray;
    }*/
}        

