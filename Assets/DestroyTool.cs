using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;

public class DestroyTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject savedObject;

    [SerializeField]private Color savedColor;

    public void UseTool()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Destroyable"))
        {
            Destroy(hit.transform.gameObject);
        }
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Destroyable"))
        {
            GameObject destroyObject = hit.transform.gameObject; //getting the object pointed at
            Material mat = destroyObject.GetComponent<Renderer>().material; //getting the material
            savedColor = mat.color; //gettimg the color
            Color color = mat.color; //making a local color
            color.a = 0.5f; //Changing the alpha of the color
            mat.color = color; //apply the color the material 
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            if (savedObject != destroyObject && savedObject != null) //Of the last object looked at isnt the currently highlighted object
            {
                savedObject.GetComponent<Renderer>().material.color = savedColor; //the color from this frame is applied
            }
            savedObject = destroyObject;
            
        }

        
    }
    public void UIToggle()
    {

    }

    void OnDisable()
    {
        if (savedObject != null)
        {
            Material mat = savedObject.GetComponent<Renderer>().material;
            Color color = mat.color;
            color.a = 1f;
            mat.color = color;
        }
        
    }

}
   


