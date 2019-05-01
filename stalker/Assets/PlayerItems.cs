using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{
    public List<Item> backpackItems = new List<Item>();
    public List<Item> beltItems = new List<Item>();
    public Item activeItem;

    public GameObject Hands; // położenie rąk
    public GameObject Backpack; // położenie plecaka
    public GameObject Belt; // Podręczny pasek przewijany scrollem

    public int BackpackCapacity = 10;
    public int BeltCapacity = 5;

    float RaycastDistance = 5.0f; // odległość chwytania
    int SelectedItemIndex = 0; // wybrany item z listy


    void Start()
    {
        
        makeActive();
        statyczne.wypiszZawartosc(beltItems);
        
    }
    void Update()
    {
        playerRaycast();
        ScrollItem();
    }
    public void playerRaycast()
    {
        RaycastHit hit;
 
        if (Physics.Raycast(transform.position, transform.forward, out hit, RaycastDistance))
        {

            GetComponent<InventoryUIScript>().showRaycastedObjectInUi(hit);
            if (Input.GetKeyDown(KeyCode.E)) // zbierz obiekt
            {
                getItem(hit);
            }
        }
    } // W CO CELUJE GRACZ
    public void getItem(RaycastHit hit)
    {
        for(int i=0; i<beltItems.Count; i++)
        {
            if (beltItems[i] == null)
            {
                beltItems[i] = hit.transform.gameObject.GetComponent<Item>();
                makeActive();
                hit.transform.position = Belt.transform.position; // Umieść go w plecaku
                hit.transform.SetParent(Belt.transform); // i przyklej go do plecaka
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true; // wyłącz fizykę obiektu
                hit.transform.gameObject.layer = 2; // zmień warstwę fizyki na 2 żeby ignorowało raycasty
                GetComponent<InventoryUIScript>().updateInventory();
                return;
            }
        }
           putItemIntoBackpack(hit);
        GetComponent<InventoryUIScript>().updateInventory();
    } // WEŹ PRZEDMIOT - do paska a jeśli pasek pełen to do plecaka
    public void putItemIntoBackpack(RaycastHit hit)
    {
        for (int i = 0; i < backpackItems.Count; i++)
        {
            if (backpackItems[i] == null)
            {
                backpackItems[i] = hit.transform.gameObject.GetComponent<Item>();
                hit.transform.position = Backpack.transform.position; // Umieść go w plecaku
                hit.transform.SetParent(Backpack.transform); // i przyklej go do plecaka
                hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true; // wyłącz fizykę obiektu
                hit.transform.gameObject.layer = 2; // zmień warstwę fizyki na 2 żeby ignorowało raycasty
                return;
            }
        }
        Debug.Log("Plecak przepełniony!");
    } // spakuj do plecaka - w pierwsze wolne miejsce
    public void ScrollItem()
    {
        if (GetComponent<PlayerMovement>().mouseMovementActive)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // gora
            {
                if (SelectedItemIndex < beltItems.Count - 1)
                {
                    SelectedItemIndex++;
                    if (beltItems[SelectedItemIndex] == null)
                    {
                        activeItem = beltItems[0];
                        Debug.Log("Wybrano: " + SelectedItemIndex + "puste");
                    }
                    else
                    {
                        makeActive();
                        Debug.Log("Wybrano: " + SelectedItemIndex + " " + beltItems[SelectedItemIndex].gameObject.name);
                    }
                    
                    
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // dol
            {
                if (SelectedItemIndex > 0)
                {
                    SelectedItemIndex--;
                    if (beltItems[SelectedItemIndex] == null)
                    {
                        activeItem = beltItems[0];
                        Debug.Log("Wybrano: " + SelectedItemIndex + "puste");
                    }
                    else
                    {
                        makeActive();
                        Debug.Log("Wybrano: " + SelectedItemIndex + " " + beltItems[SelectedItemIndex].gameObject.name);
                    }
                    
                    
                }

            }
        }
    } // Zmiana aktywnego itemu skrolowaniem paska
    public void makeActive()
    {
        activeItem = beltItems[SelectedItemIndex];

        GetComponent<InventoryUIScript>().updateSelectedItemPreview();
    } // faktycznie złap selectedItem do łapy

}
