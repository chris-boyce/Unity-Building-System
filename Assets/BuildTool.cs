using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class BuildTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject buildUI;
    private bool uiToggleInput;
    [SerializeField] private PrimitiveType spawnShape;
    private GameObject tempShape;
    public GameObject model;

    void OnEnable()
    {
        ShapeSelector(PrimitiveType.Cube);
    }
    public void UseTool()
    {
        if (buildUI.activeSelf) //Check for UI
            return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            tempShape.GetComponent<ObjectVisuals>().MakeObjectTransparent(1f);
            tempShape.GetComponent<ObjectVisuals>().RaycastsEnabled();

            tempShape = null;
            ShapeSelector(spawnShape);
        }


    }

    void Update()
    {
        UIToggle();
        TempCubePlacement();
    }
    private void TempCubePlacement()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = new Vector3(Mathf.Round(hit.point.x + 0.5f), Mathf.Round(hit.point.y + 0.5f), MathF.Round(hit.point.z));
            tempShape.transform.position = hitPoint;
            GameObject hitObject = hit.collider.gameObject;
        }
    }
    public void UIToggle()
    {
        //Brings Up and Down Menu also disables the players Cursor movement and allows UI to Be Clicked
        uiToggleInput |= (Input.GetKey(KeyCode.Tab));
        if (uiToggleInput)
        {
            buildUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
            
        }
        else
        {
            buildUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
        }
        uiToggleInput = false;
    }
    public void ShapeSelector(PrimitiveType shapeToSpawn)
    {
        //Sets a temp shape that show where it is going to be placed 
        Destroy(tempShape);
        spawnShape = shapeToSpawn;
        tempShape = GameObject.CreatePrimitive(spawnShape);
        tempShape.AddComponent<ObjectVisuals>().MakeObjectTransparent(0.5f);
        tempShape.GetComponent<ObjectVisuals>().RaycastsDisabled();
    }

    public void ModelSelector(GameObject prefab)
    {
        Destroy(tempShape);
        tempShape = Instantiate(prefab);
        tempShape.AddComponent<ObjectVisuals>().MakeObjectTransparent(0.5f);
        tempShape.GetComponent<ObjectVisuals>().RaycastsDisabled();
    }
    

    void OnDisable()
    {
        Destroy(tempShape);
    }
    

}

