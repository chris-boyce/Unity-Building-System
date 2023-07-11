using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelector : MonoBehaviour
{
    //Amount of Tool 3
    [SerializeField] private int minNumber = 0;
    [SerializeField] private  int maxNumber = 2;

    //Get the GO and then the Interface
    [SerializeField] private GameObject[] playerTools;
    private IToolable[] Tools;
    
    //Currently Equipped Tool
    public int toolIDNum;
    void Start()
    {
        //Fills Tool Array and Disables then
        Tools = new IToolable[maxNumber + 1];
        for (int i = 0; i < maxNumber + 1; i++)
        {
            Tools[i] = playerTools[i].GetComponent<IToolable>();
        }
        ChangeTool();
    }
    void Update()
    {
        // Scroll To Change Between Weapon and Enables and Disables The Tools in Hands
        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0f) 
        {
            toolIDNum++;
            ChangeTool();
            
        }
        else if (scroll < 0f) 
        {
            toolIDNum--;
            ChangeTool();
        }
        toolIDNum = Mathf.Clamp(toolIDNum, minNumber, maxNumber);

        //Calls the Use Function on the Tools depending on the one in hand
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ButtonPressed");
            Tools[toolIDNum].UseTool();
        }

    }

    //Visuals
    private void ChangeTool()
    {
        
        if (toolIDNum == 0)
        {
            DisableTools();
            playerTools[0].SetActive(true);
        }
        else if (toolIDNum == 1)
        {
            DisableTools();
            playerTools[1].SetActive(true);
        }
        else if (toolIDNum == 2)
        {
            DisableTools();
            playerTools[2].SetActive(true);
        }
    }

    private void DisableTools()
    {
        for (int i = 0; i < maxNumber +1 ; i++)
        {
            playerTools[i].SetActive(false);
        }
    }
}
