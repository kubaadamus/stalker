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
        updateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }


    }
    public void ToggleInventory()
    {
        updateInventory();
        InventoryVisible = !InventoryVisible;
        InventoryUI.SetActive(InventoryVisible);
        GetComponent<PlayerMovement>().mouseMovementActive = !InventoryVisible;

        if (InventoryVisible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
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
        if (GetComponent<PlayerItems>().activeItem != null)
        {
            selectedItemPreview.GetComponent<MeshFilter>().mesh = GetComponent<PlayerItems>().activeItem.GetComponent<MeshFilter>().sharedMesh;
            selectedItemName.GetComponent<Text>().text = GetComponent<PlayerItems>().activeItem.gameObject.name;
        }

    }
    public void updateInventory()
    {
        foreach (Transform child in backpackScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in beltScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // RYSOWANIE PLECAKA
        List<Item> backpackItemsList = GetComponent<PlayerItems>().backpackItems;

        int row = 0;
        int col = 0;

        foreach (Item item in backpackItemsList)
        {
            if (row > 4)
            {
                row = 0;
                col++;
            }

            Button newInventoryButton = Instantiate(inventoryButton, backpackScrollViewContent.transform.position, Quaternion.Euler(0, 0, 0));
            newInventoryButton.transform.SetParent(backpackScrollViewContent.transform);
            newInventoryButton.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newInventoryButton.transform.localScale = new Vector3(1, 1, 1);
            newInventoryButton.name = "UiButton_" + row + "_" + col;
            Text newInventoryButtonText = newInventoryButton.GetComponentInChildren<Text>();
            if (item != null)
            {
                newInventoryButtonText.text = item.GetComponent<Item>().name;
            }
            else
            {
                newInventoryButtonText.text = "empty";
            }
            //pozycja zależna od kolejności na liście
            newInventoryButton.transform.localPosition = new Vector3(50 + row * 50, -50 - col * 50, 0);
            row++;
        }

        row = 0;
        // RYSOWANIE PASKA PODRĘCZNEGO
        List<Item> beltItemsList = GetComponent<PlayerItems>().beltItems;

        foreach (Item item in beltItemsList)
        {
           
                Button newInventoryButton = Instantiate(inventoryButton, beltScrollViewContent.transform.position, Quaternion.Euler(0, 0, 0));
                newInventoryButton.transform.SetParent(beltScrollViewContent.transform);
                newInventoryButton.transform.localRotation = Quaternion.Euler(0, 0, 0);
                newInventoryButton.transform.localScale = new Vector3(1, 1, 1);
                newInventoryButton.name = "UiButton_" + row;
                Text newInventoryButtonText = newInventoryButton.GetComponentInChildren<Text>();
            if (item != null)
            {
                newInventoryButtonText.text = item.GetComponent<Item>().name;
            }
            else
            {
                newInventoryButtonText.text = "empty";
            }
            //pozycja zależna od kolejności na liście
            newInventoryButton.transform.localPosition = new Vector3(50 + row * 50, -50, 0);
                row++;
           
        }
    }
}
