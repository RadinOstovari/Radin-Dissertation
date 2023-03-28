using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 60.0f; // Total time for the timer
    private Text timerText; // The text object that displays the timer

    void Start()
    {
        // Get the Text component attached to the same GameObject as this script
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        // Decrease time remaining
        timeRemaining -= Time.deltaTime;

        // Update the timer text
        int minutes = Mathf.FloorToInt(timeRemaining / 60.0f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60.0f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;

        // Check if time is up
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            // Do something when the timer reaches 0, such as calling a method or changing a scene
        }
    }
}
