using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTool : MonoBehaviour, IToolable
{
    private bool uiToggleInput;
    void Start()
    {
        uiToggleInput = false;
    }
    public void UseTool()
    {
        
    }

    
    void Update()
    {
        uiToggleInput |= (Input.GetKey(KeyCode.Tab));
        if (uiToggleInput)
        {

        }
    }
}
