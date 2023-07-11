using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject BuildUI;
    private bool uiToggleInput;
    [SerializeField] private PrimitiveType spawnShape;
    private GameObject tempShape;

    void Start()
    {
        ShapeSelector(PrimitiveType.Cube);
    }
    public void UseTool()
    {
        if (!BuildUI.activeSelf)
        {
            Debug.Log("Is USED");
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); // Create a ray from the center of the screen

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // An object was hit
                Vector3 hitPoint = hit.point;
                tempShape = null;
                ShapeSelector(spawnShape);
                
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Object hit: " + hitObject.name);

            }
        }

    }

    void Update()
    {
        UIToggle();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); // Create a ray from the center of the screen

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // An object was hit
            Vector3 hitPoint = hit.point;
            tempShape.transform.position = hit.point;
            GameObject hitObject = hit.collider.gameObject;
        }

    }

    
    private void UIToggle()
    {
        //Brings Up and Down Menu also disables the players Cursor movement and allows UI to Be Clicked
        uiToggleInput |= (Input.GetKey(KeyCode.Tab));
        if (uiToggleInput)
        {
            BuildUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
            
        }
        else
        {
            BuildUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
        }
        uiToggleInput = false;
    }
    public void ShapeSelector(PrimitiveType shapeToSpawn)
    {
        Destroy(tempShape);
        spawnShape = shapeToSpawn;
        tempShape = GameObject.CreatePrimitive(spawnShape);
        
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        tempShape.layer = LayerIgnoreRaycast;

        TransparentShape();
        
    }

    void TransparentShape()
    {
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
    

}

