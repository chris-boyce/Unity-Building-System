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
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Editable"))
        {
            Destroy(hit.transform.gameObject);
        }
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Editable"))
        {
            GameObject destroyObject = hit.transform.gameObject; //getting the object pointed at
            destroyObject.GetComponent<ObjectVisuals>().MakeObjectTransparent(0.5f);
            if (savedObject != destroyObject && savedObject != null) //Of the last object looked at isnt the currently highlighted object
            {
                savedObject.GetComponent<ObjectVisuals>().MakeObjectTransparent(1f);  //the color from this frame is applied
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
            savedObject.GetComponent<ObjectVisuals>().MakeObjectTransparent(1f);
        }
        
    }

}
   


