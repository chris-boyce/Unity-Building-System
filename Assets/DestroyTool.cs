using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;

public class DestroyTool : MonoBehaviour, IToolable
{
    private GameObject savedObject;

    private Color savedColor;

    public void UseTool()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Destroyable"))
        {
            Destroy(hit.transform.gameObject);
        }
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Destroyable"))
        {
            GameObject destroyObject = hit.transform.gameObject;
            Material mat = destroyObject.GetComponent<Renderer>().material;
            savedColor = mat.color;
            mat.color = Color.red;
            Color color = mat.color;
            color.a = 0.5f;
            mat.color = color;

            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            if (savedObject != destroyObject && savedObject != null)
            {
                savedObject.GetComponent<Renderer>().material.color = savedColor;
            }
            savedObject = destroyObject;
        }

        
    }
    public void UIToggle()
    {

    }

}
   


