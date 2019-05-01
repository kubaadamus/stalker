using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : Item
{
    public bool thrown = false;
    public int seconds = 5;
    void Start()
    {
        InvokeRepeating("Countdown", 0.0f, 1.0f);
    }

    
    void Update()
    {
        
    }

    public override void Use()
    {
        Debug.Log("Wyrzuucam granat!");
        thrown = true;
        gripable = false;
        //gameObject.transform.root.GetComponent<PlayerItems>().DropItemFromBelt();
    }
    public void Countdown()
    {
        if(thrown)
        {
            seconds -= 1;
            if(seconds==0)
            {
                Debug.Log("BOOM!");
                Destroy(this.gameObject);
            }
        }
    }
}
