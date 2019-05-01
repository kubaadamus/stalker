using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    public GameObject backpackScrollViewContent;
    public GameObject beltScrollViewContent;
    public GameObject InventoryUI; // element do którego przyczepiony jest cały canvas i ui, jest disabled albo enabled
    public Button inventoryButton;
    public bool InventoryVisible = false;

    //UI
    public GameObject rayastedItemName; // tekst UI mówiący w co wycelował gracz
    public GameObject selectedItemPreview;   // model 3D tego co ma wybrane gracz
    public GameObject selectedItemName; // nazwa przedmiotu wybranego przez gracza

    void Start()
    {
        HideInventory();
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventory();
        
    }
    public void ShowInventory()
    {
        // RYSOWANIE PLECAKA
        List<Item> backpackItemsList = GetComponent<PlayerItems>().backpackItems;

        int row = 0;
        int col = 0;
        foreach(Item item in backpackItemsList)
        {
            if(item != null)
            {
                Button newInventoryButton = Instantiate(inventoryButton, backpackScrollViewContent.transform.position, Quaternion.Euler(0, 0, 0));
                newInventoryButton.transform.SetParent(backpackScrollViewContent.transform);
                newInventoryButton.transform.localRotation = Quaternion.Euler(0, 0, 0);
                newInventoryButton.transform.localScale = new Vector3(1, 1, 1);
                Text newInventoryButtonText = newInventoryButton.GetComponentInChildren<Text>();
                newInventoryButtonText.text = item.GetComponent<Item>().name;
                //pozycja zależna od kolejności na liście
                newInventoryButton.transform.localPosition = new Vector3(50 + row * 50, -50 - col * 50, 0);
                row++;
                if (row > 6)
                {
                    row = 0;
                    col++;
                }
            }
        }

        row = 0;
        // RYSOWANIE PASKA PODRĘCZNEGO
        List<Item> beltItemsList = GetComponent<PlayerItems>().beltItems;

        foreach (Item item in beltItemsList)
        {
            if (item != null)
            {
                Button newInventoryButton = Instantiate(inventoryButton, beltScrollViewContent.transform.position, Quaternion.Euler(0, 0, 0));
                newInventoryButton.transform.SetParent(beltScrollViewContent.transform);
                newInventoryButton.transform.localRotation = Quaternion.Euler(0, 0, 0);
                newInventoryButton.transform.localScale = new Vector3(1, 1, 1);
                Text newInventoryButtonText = newInventoryButton.GetComponentInChildren<Text>();
                newInventoryButtonText.text = item.GetComponent<Item>().name;
                //pozycja zależna od kolejności na liście
                newInventoryButton.transform.localPosition = new Vector3(50 + row * 50, -50, 0);
                row++;
            }
        }
    }
    public void HideInventory()
    {
        foreach (Transform child in backpackScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryVisible = !InventoryVisible;
            InventoryUI.SetActive(InventoryVisible);
            GetComponent<PlayerMovement>().mouseMovementActive = !InventoryVisible;
            
            if(InventoryVisible)
            {
                Cursor.lockState = CursorLockMode.None;
                ShowInventory();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                HideInventory();
            }
        }
    }
    public void showRaycastedObjectInUi(RaycastHit hit)
    {
        if (hit.transform.gameObject != null)
        {
            rayastedItemName.GetComponent<Text>().text = hit.transform.gameObject.name; //Wyświetl nazwę namierzonego obiektu
        }
        else
        {
            rayastedItemName.GetComponent<Text>().text = "";
        }
    }
    public void updateSelectedItemPreview()
    {
        if (GetComponent<PlayerItems>().activeItem!=null)
        {
            selectedItemPreview.GetComponent<MeshFilter>().mesh = GetComponent<PlayerItems>().activeItem.GetComponent<MeshFilter>().sharedMesh;
            selectedItemName.GetComponent<Text>().text = GetComponent<PlayerItems>().activeItem.gameObject.name;
        }

    }
}
