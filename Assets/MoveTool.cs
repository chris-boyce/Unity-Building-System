using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTool : MonoBehaviour, IToolable
{
    [SerializeField]private bool hasObject;
    [SerializeField] private GameObject moveObject;
    
    public void UseTool()
    {
        if (!hasObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Destroyable"))
            {
                moveObject = hit.transform.gameObject;
                hasObject = true;
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 1), MathF.Round(hit.point.z));
                moveObject.transform.position = hitPoint;
                hasObject = false;
                
            }
        }
        
    }

    public void UIToggle()
    {

    }
}
