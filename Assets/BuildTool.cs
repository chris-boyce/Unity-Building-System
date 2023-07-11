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

    void OnEnable()
    {
        ShapeSelector(PrimitiveType.Cube);
    }
    public void UseTool()
    {
        if (!buildUI.activeSelf) //Check for UI
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                tempShape.GetComponent<Renderer>().material.color = Color.white;
                tempShape.transform.parent = null;
                tempShape.tag = "Destroyable";
                int LayerDefault = LayerMask.NameToLayer("Default");
                tempShape.layer = LayerDefault;
                
                tempShape = null;
                ShapeSelector(spawnShape);
            }
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

            Vector3 hitPoint = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 1), MathF.Round(hit.point.z));
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
        //Cube Will build ontop of itself if has raycasts enabled
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        tempShape.layer = LayerIgnoreRaycast;
        TransparentShape();
        
    }
    void TransparentShape()
    {
        //Color to Transparent
        Material mat = tempShape.GetComponent<Renderer>().material;
        mat.color = Color.gray;

        Color color = mat.color;
        color.a = 0.5f;
        mat.color = color;

        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    void OnDisable()
    {
        Destroy(tempShape);
    }
    

}

