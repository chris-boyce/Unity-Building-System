using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisuals : MonoBehaviour
{
    private Material mat;

    private int layerDefault;
    private int layerIgnoreRaycast;
    private bool isColliders;

    void Awake()
    {
        //Control Variables  
        isColliders = true;
        layerDefault = LayerMask.NameToLayer("Default");
        layerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.tag = "Editable";

        //Setting Up Material To be edited
        mat = GetComponent<Renderer>().material;
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    #region ObjectFunctions
    public void AddEmissions(float emissionMultipler)
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", mat.color * emissionMultipler);
    }

    public void MakeObjectTransparent(float transparentValue)
    {
        Color color = mat.color;
        color.a = transparentValue;
        mat.color = color;
    }

    public void RotateObject(float degrees)
    {
        gameObject.transform.Rotate(new Vector3(0, degrees, 0));
    }

    public void ScaleObject(float scaleMultipler)
    {
        gameObject.transform.localScale = new Vector3(scaleMultipler, scaleMultipler, scaleMultipler);
    }

    public void ColorChange(Color selectedColor)
    {
        mat.color = selectedColor;
    }

    public void ToggleRB() //Rb Tool Toggle (You cant disable RBs so have to destroy)
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    public void ToggleColliders()
    {
        isColliders =! isColliders;
        GetComponent<Collider>().enabled = isColliders;
    }

    public void RaycastsEnabled()
    {
        gameObject.layer = layerDefault;
    }

    public void RaycastsDisabled()
    {
        gameObject.layer = layerIgnoreRaycast;
    }
    #endregion ObjectFunctions


}
