using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController ctrl;
    //Sterowanie myszką//
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    public bool mouseMovementActive = true;
    public bool keyboardMovementActive = true;
    //Sterowanie klawiaturą//
    public float jumpFactor = 4.0f;
    public float speed = 5.0f;
    Vector3 movement = Vector3.zero;

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        MouseMovement();
        KeyboardMovement();
    }
    public void MouseMovement()
    {
        if(mouseMovementActive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");
            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);  //  Ograniczenie ruchu kamery
            transform.rotation = Quaternion.Euler(rotX, rotY, 0);   //  Obrócenie kamery
        }

    }
    public void KeyboardMovement()
    {
        if(keyboardMovementActive)
        {
            if (!ctrl.isGrounded)   //Jeśli gracz nie stoi na ziemi to grawitacja go do niej przyciąga.
            {
                movement.y += Physics.gravity.y * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space) && ctrl.isGrounded) //Skakanie                               
            {
                movement.y = jumpFactor;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) //Bieganie ON
            {
                speed = 8.0f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))  //Bieganie OFF
            {
                speed = 5.0f;
            }
            if (ctrl.isGrounded)    //Ustawianie ruchu                                                            
            {
                movement.z = Input.GetAxis("Vertical") * speed * transform.forward.z - Input.GetAxis("Horizontal") * speed * transform.forward.x;   //korzystamy z globalnego układu współrzędnych. jak obróci się bokiem do osi Z to będzie działał horizontal
                movement.x = Input.GetAxis("Vertical") * speed * transform.forward.x + Input.GetAxis("Horizontal") * speed * transform.forward.z;
            }
            ctrl.Move(movement * Time.deltaTime);   //Wykonanie ruchu
        }
        
    }
}
