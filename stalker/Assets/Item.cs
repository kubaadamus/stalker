using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public bool gripable = true; // czy można go złapać, np odbezpieczonego granatu nie można złapać
    public string name;
    public float damage;
    public float health;
    public int loadedAmmo; // ilość amunicji załadowana do przedmiotu
    public int maxAmmo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public virtual void Reload()
    {
        Debug.Log("odpalam ogólne reload ");
    }
    public virtual void Use()
    {
        Debug.Log("odpalam ogolna wersje Use");
    }
    public virtual void showStats()
    {

    }
}
