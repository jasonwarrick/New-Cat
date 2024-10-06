using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameClock : MonoBehaviour
{
    [Tooltip("The number of seconds it takes to cause a tick of the clock")]
    [SerializeField] float tickFrequency;
    [Tooltip("The number of seconds time moves forwards by on each tick")]
    [SerializeField] int tickFactor;

    int minutes = 0;
    int minutesCap = 59;
    int hours = 0;
    int hoursCap = 12;
    float timeCounter = 0f;
    public bool isClockStopped = false;
    string currentTime;


    void Start() {
        SetTime(10, 0);
    }
    
    public void SetTime(int newHour, int newMinute) {
        if (newHour > hoursCap || newMinute > minutesCap) { return; }

        hours = newHour;
        minutes = newMinute;
    }

    void IncrementTime() {
        minutes += tickFactor;

        if (minutes > minutesCap) {
            minutes = 0;
            hours += 1;
        }

        hours = hours > hoursCap ? 1 : hours; // If the hours exceeds the cap, set it to 1

        if (minutes < 10) {
            currentTime = hours + ":0" + minutes;
        } else {
            currentTime = hours + ":" + minutes;
        }

        
        Debug.Log(currentTime);

    }

    void Update() {
        if (!isClockStopped) {
            timeCounter += Time.deltaTime;

            if (timeCounter >= tickFrequency) {
                timeCounter = 0f;
                IncrementTime();
            }
        }
    }
}
