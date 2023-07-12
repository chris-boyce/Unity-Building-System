using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditUIButton : MonoBehaviour
{
    public Enums.EditFunctions Jobs;
    public EditTool EditTool;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeJob);
    }


    void ChangeJob()
    {
        switch (Jobs)
        {
            case Enums.EditFunctions.Color:
                EditTool.job = Enums.EditFunctions.Color;
                break;
            case Enums.EditFunctions.RigidbodyToggles:
                EditTool.job = Enums.EditFunctions.RigidbodyToggles;
                break;


        }
    }
}
