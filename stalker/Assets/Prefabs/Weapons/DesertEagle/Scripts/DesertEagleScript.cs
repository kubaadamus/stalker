using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagleScript : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Use()
    {
        if (loadedAmmo > 0)
        {
            loadedAmmo -= 1;
            Debug.Log("Desert BOOM!, zostało: " + loadedAmmo);
        }
        else
        {
            Debug.Log("Cyk xD");
        }
    }
    public override void Reload() // Przeszukuje listę Itemów w poszukiwaniu tych co zawierają słowo DesertEagleAmmo i po kolei pobiera z nich tyle ammo ile potrzeba lub się da
    {
        List<Item> lista = gameObject.transform.root.GetComponent<PlayerItems>().backpackItems;
        foreach (Item item in lista)
        {
            if(item.gameObject.name.Contains("DesertEagleAmmo"))
            {
                int ammoNeeded = maxAmmo - loadedAmmo;
                
                if (ammoNeeded > item.loadedAmmo)
                {
                    loadedAmmo += item.loadedAmmo;
                    item.loadedAmmo = 0;
                }
                else
                {
                    loadedAmmo += ammoNeeded;
                    item.loadedAmmo -= ammoNeeded;
                }
            }
        }

    }
}