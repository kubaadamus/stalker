using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int seconds = 0;
    public Timer(int seconds)
    {
        this.seconds = seconds;
    }
    void Start()
    {
        InvokeRepeating("Countdown", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Countdown()
    {
        seconds -= 1;
        Debug.Log("TICK");
        if(seconds==0)
        {
            Debug.Log("BOOM!");
            Destroy(this);
        }
    }
}
