using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditUIButton : MonoBehaviour
{
    [SerializeField] private Enums.EditFunctions Jobs;
    [SerializeField] private EditTool EditTool;
    [SerializeField] private TMP_Text degreeText;
    [SerializeField] private AudioClip sound_UIClick;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeJob);
    }

    void OnEnable()
    {
        if(sound_UIClick != null)
            SoundManager.instance?.PlaySoundEffect(sound_UIClick);
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
            case Enums.EditFunctions.Transparent:
                EditTool.job = Enums.EditFunctions.Transparent;
                break;
            case Enums.EditFunctions.Scale:
                EditTool.job = Enums.EditFunctions.Scale;
                break;
        }
    }

    public void DegreeText(float degrees)
    {
        degreeText.text = degrees.ToString();
    }
}
