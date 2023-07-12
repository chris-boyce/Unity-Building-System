using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
using Color = UnityEngine.Color;

public class EditTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject editUI;
    public Enums.EditFunctions job;
    private bool uiToggleInput;
    public Color selectedColor;
    public float rotationAmount;
    public float emissionMultipler;
    void Start()
    {
        uiToggleInput = false;
        
    }
    public void UseTool()
    {
        if (editUI.activeSelf) //UI Checker
            return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Editable"))
        {
            switch (job)
            {
                case Enums.EditFunctions.Color:
                    ColorChange(hit);
                    break;
                case Enums.EditFunctions.RigidbodyToggles:
                    ToggleRB(hit);
                    break;
                case Enums.EditFunctions.Rotation:
                    RotateObject(hit);
                    break;
                case Enums.EditFunctions.Emmissions:
                    AddEmmisions(hit);
                    break;

            }
        }
    }

    public void RotateObject(RaycastHit hit)
    {
        hit.collider.gameObject.GetComponent<Transform>().Rotate(new Vector3(0 , rotationAmount, 0));
    }

    public void AddEmmisions(RaycastHit hit)
    {
        hit.collider.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", hit.collider.gameObject.GetComponent<Renderer>().material.color * emissionMultipler);
    }

    void ColorChange(RaycastHit hit) 
    {
        hit.collider.gameObject.GetComponent<Renderer>().material.color = selectedColor;
    }
    public void RotationChange(float degrees)
    {
        rotationAmount = Mathf.Round(degrees);
    }

    public void EmissionIntensityChange(float strength)
    {
        emissionMultipler = Mathf.Round(strength);
    }

    void ToggleRB(RaycastHit hit) //Rb Tool Toggle (You cant disable RBs so have to destroy)
    {
        if (hit.collider.gameObject.GetComponent<Rigidbody>() == null)
        {
            hit.collider.gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            Destroy(hit.collider.gameObject.GetComponent<Rigidbody>());
        }
        
    }

    void Update()
    {
        UIToggle();
    }

    public void UIToggle()
    {
        uiToggleInput |= (Input.GetKey(KeyCode.Tab));
        if (uiToggleInput)
        {
            editUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
        }
        else
        {
            editUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
        }
        uiToggleInput = false;
    }
}
