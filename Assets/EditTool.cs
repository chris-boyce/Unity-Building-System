using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;
using Color = UnityEngine.Color;

public class EditTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject editUI;
    public Enums.EditFunctions job;
    private bool uiToggleInput;

    [SerializeField] private AudioClip sound_Edit;

    public Color selectedColor;
    public float rotationAmount;
    public float emissionMultipler;
    public float transparentMultipler;
    public float scaleMultipler;
    void Start()
    {
        uiToggleInput = false;
    }
    public void UseTool()
    {
        if (editUI.activeSelf) //UI Checker
            return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Editable"))
        {
            ObjectVisuals objVisuals = hit.collider.gameObject.GetComponent<ObjectVisuals>();
            switch (job)
            {
                case Enums.EditFunctions.Color:
                    objVisuals.ColorChange(selectedColor);
                    break;
                case Enums.EditFunctions.RigidbodyToggles:
                    objVisuals.ToggleRB();
                    break;
                case Enums.EditFunctions.Rotation:
                    objVisuals.RotateObject(rotationAmount);
                    break;
                case Enums.EditFunctions.Emmissions:
                    objVisuals.AddEmissions(emissionMultipler);
                    break;
                case Enums.EditFunctions.Transparent:
                    objVisuals.MakeObjectTransparent(transparentMultipler);
                    break;
                case Enums.EditFunctions.Scale:
                    objVisuals.ScaleObject(scaleMultipler);
                    break;

            }
            SoundManager.instance.PlaySoundEffect(sound_Edit);
        }
    }
    //Saves Local Variable To The Tool For When the Key Tool Is Used

    #region LocalVariableFunctions

    public void RotationChange(float degrees)
    {
        rotationAmount = Mathf.Round(degrees);
    }

    public void EmissionIntensityChange(float strength)
    {
        emissionMultipler = Mathf.Round(strength);
    }

    public void TransparentChange(float strength)
    {
        transparentMultipler = strength;
    }

    public void ScaleChange(float scale)
    {
        scaleMultipler = scale;
    }
    # endregion LocalVariableFunctions

    void Update()
    {
        UIToggle();
    }

    public void UIToggle()
    {
        GameObject.FindGameObjectWithTag("UIText").GetComponent<TMP_Text>().text = "Edit Tool";
        uiToggleInput |= (Input.GetKey(KeyCode.Tab));
        if (uiToggleInput)
        {
            editUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
        }
        else
        {
            editUI.SetActive(uiToggleInput);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = uiToggleInput;
            PlayerController.Instance.disableCursor = uiToggleInput;
        }
        uiToggleInput = false;
    }
}
