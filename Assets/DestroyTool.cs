using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyTool : MonoBehaviour, IToolable
{
    [SerializeField] private GameObject savedObject;
    [SerializeField]private Color savedColor;

    [SerializeField] private AudioClip sound_Destroy;
    void OnEnable()
    {
        UIToggle();
    }
    public void UseTool()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Editable"))
        {
            Destroy(hit.transform.gameObject);
            SoundManager.instance.PlaySoundEffect(sound_Destroy);
        }
    }

    void Update()
    {
        DestroyRaycast();
    }

    void DestroyRaycast()
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
        GameObject.FindGameObjectWithTag("UIText").GetComponent<TMP_Text>().text = "Destroy Tool";
    }

    void OnDisable()
    {
        if (savedObject != null)
        {
            savedObject.GetComponent<ObjectVisuals>().MakeObjectTransparent(1f);
        }
        
    }

}
   


