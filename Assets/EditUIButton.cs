using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditUIButton : MonoBehaviour
{
    public Enums.EditFunctions Jobs;
    public EditTool EditTool;
    [SerializeField] private TMP_Text degreeText;
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
            case Enums.EditFunctions.Rotation:
                EditTool.job = Enums.EditFunctions.Rotation;
                break;
            case Enums.EditFunctions.Emmissions:
                EditTool.job = Enums.EditFunctions.Emmissions;
                break;


        }
    }

    public void DegreeText(float degrees)
    {
        degreeText.text = degrees.ToString();
    }
}
