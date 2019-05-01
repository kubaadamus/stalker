using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedikitScript : Item
{
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public override void Use()
    {
        if(loadedAmmo>0)
        {
            Debug.Log("Używam Medikita");
            loadedAmmo--;
        }
        else
        {
            Debug.Log("Medikit zużyty");
        }


    }
}
