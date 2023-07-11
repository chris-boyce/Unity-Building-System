using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    public bool disableCursor;

    public float sens = 2f;
    public float lookXLimit = 45f;

    public float curSpeedX;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    private bool sprintInput;

    void Start()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sprintInput = false;
    }
    void Update()
    {
        sprintInput |= (Input.GetKey(KeyCode.LeftShift));
        if (sprintInput && Input.GetAxis("Vertical") > 0.1)
        {
            curSpeedX = runSpeed * Input.GetAxis("Vertical");
            sprintInput = false;
        }
        else
        {
            curSpeedX = walkSpeed * Input.GetAxis("Vertical");
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);


        float curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        characterController.Move(moveDirection * Time.deltaTime);

        if (!disableCursor)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sens;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sens, 0);
        }
        
    }
}
