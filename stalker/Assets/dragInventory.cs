using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragInventory : MonoBehaviour
{
    //public List<Item> backpackItems;
    //public List<Item> beltItems;
    //public Item activeItem;
    public Canvas myCanvas;
    public Button selectedButton;
    public Transform parent;
    public Vector3 position;
    int siblingIndex;
    void Start()
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            myCanvas.transform as RectTransform, Input.mousePosition,
            myCanvas.worldCamera,
            out pos);
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.GetComponent<Button>() && hit.transform.gameObject.GetComponentInChildren<Text>().text != "melee")
                {
                    Debug.Log(hit.transform.name);
                    selectedButton = hit.transform.GetComponent<Button>();
                    position = selectedButton.transform.position;
                    parent = selectedButton.transform.parent;
                    siblingIndex = selectedButton.transform.GetSiblingIndex();
                    selectedButton.transform.parent = GameObject.Find("InventoryUI").transform;
                    selectedButton.gameObject.layer = 2;
                    Debug.Log("Chwytam obiekt " + selectedButton.gameObject.name + " czyli obiekt z: " + parent.gameObject.name + " o indeksie " + siblingIndex);
                }
            }

           
        }

        if(selectedButton!=null)
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                myCanvas.transform as RectTransform,
                Input.mousePosition, myCanvas.worldCamera,
                out movePos);

            selectedButton.transform.position = myCanvas.transform.TransformPoint(movePos);
        }
        if(Input.GetMouseButtonUp(0) && selectedButton != null)
        {

            



            
            selectedButton.transform.parent = parent;
            selectedButton.transform.position = position;
            selectedButton.transform.SetSiblingIndex(siblingIndex);
            


            //Jesli upuszczasz to zraycastuj to co jest pod myszą
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Button>() && hit.transform.gameObject.GetComponentInChildren<Text>().text != "melee")
                {
                    Debug.Log("Upuszczam na obiekt: "+hit.transform.gameObject.name + " czyli obiekt z: " +hit.transform.parent.gameObject.name + " o indeksie " + hit.transform.GetSiblingIndex());
                    //Destroy(hit.transform.gameObject);
                }
                else
                {
                    Debug.Log("Pod spodem było coś ale to nie był przycisk lub chciałem przemieścić melee");
                    
                }
            }
            else
            {
                Debug.Log("Pod spodem nie było nic :<");
            }


            selectedButton.gameObject.layer = 5;
            selectedButton = null;
           


        }

       //Debug.Log(transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition));

    }
    public void getUpdatedItems()
    {
        //backpackItems = GetComponent<PlayerItems>().backpackItems;
        //beltItems = GetComponent<PlayerItems>().beltItems;
        //activeItem = GetComponent<PlayerItems>().activeItem;
    }
    public void clicked()
    {
        Debug.Log("TAK");
    }




}
