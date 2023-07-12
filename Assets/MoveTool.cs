using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveTool : MonoBehaviour, IToolable
{
    [SerializeField] private bool hasObject;
    [SerializeField] private GameObject moveObject;
    [SerializeField] private AudioClip sound_Move;

    void OnEnable()
    {
        UIToggle();
    }
    public void UseTool()
    {
        if (!hasObject) //First Press
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Editable"))
            {
                moveObject = hit.transform.gameObject;
                moveObject.GetComponent<ObjectVisuals>().RaycastsDisabled();
                moveObject.GetComponent<ObjectVisuals>().MakeObjectTransparent(0.5f);
                moveObject.GetComponent<ObjectVisuals>().ToggleColliders();
                hasObject = true;
                SoundManager.instance?.PlaySoundEffect(sound_Move);
            }
        }
        else //Second Press
        {
            moveObject.GetComponent<ObjectVisuals>().RaycastsEnabled();
            moveObject.GetComponent<ObjectVisuals>().MakeObjectTransparent(1f);
            moveObject.GetComponent<ObjectVisuals>().ToggleColliders();
            hasObject = false;
            SoundManager.instance?.PlaySoundEffect(sound_Move);
        }
        
    }

    void Update()
    {
        if (!hasObject)
            return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 1),
                MathF.Round(hit.point.z));
            moveObject.transform.position = hitPoint;
        }

    }

    void OnDisable()
    {
        if (moveObject != null)
        {
            moveObject.GetComponent<ObjectVisuals>().RaycastsEnabled();
            hasObject = false;
        }
    }

    public void UIToggle()
    {
        GameObject.FindGameObjectWithTag("UIText").GetComponent<TMP_Text>().text = "Move Tool";
    }
}
