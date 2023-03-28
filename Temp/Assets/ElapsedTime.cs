using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElapsedTime : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float delay = 5f;

    private float startTime;
    private float elapsedTime;
    private bool isTimerStarted = false;

    void Start()
    {
        Invoke("StartTimer", delay);
    }

    void Update()
    {
        if (isTimerStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Check if the space bar is pressed
            {
                StopTimer();
            }
            else
            {
                elapsedTime = Time.time - startTime;
                float seconds = Mathf.Floor(elapsedTime * 100) / 100f; // two decimal points
                timerText.text = "Time: " + seconds.ToString("F2"); // format to two decimal points
            }
        }
    }

    void StartTimer()
    {
        startTime = Time.time;
        isTimerStarted = true;
    }

    void StopTimer()
    {
        isTimerStarted = false;
    }
}


