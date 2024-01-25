using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject buildUI;
    [SerializeField] private GameObject model;
    [SerializeField] private PrimitiveType spawnShape;
    [SerializeField] private AudioClip sound_Place;
    

    private bool uiToggleInput;
    private GameObject tempShape;
    private bool isPrimative;

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

            SoundManager.instance.PlaySoundEffect(sound_Place);

            if (isPrimative)
                ShapeSelector(spawnShape);
            else  
                ModelSelector(model);
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
        }
    }
    public void UIToggle()
    {
        GameObject.FindGameObjectWithTag("UIText").GetComponent<TMP_Text>().text = "Build Tool";
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
        TempShapeInit(tempShape);
        isPrimative = true;
    }

    public void ModelSelector(GameObject prefab)
    {
        Destroy(tempShape);
        model = prefab;
        tempShape = Instantiate(prefab);
        TempShapeInit(tempShape);
        isPrimative = false;
        
    }

    void TempShapeInit(GameObject GO)
    {
        GO.AddComponent<ObjectVisuals>().MakeObjectTransparent(0.5f);
        GO.GetComponent<ObjectVisuals>().RaycastsDisabled();
    }
    
    void OnDisable()
    {
        Destroy(tempShape);
    }
    

}

