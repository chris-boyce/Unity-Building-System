using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ObjectVisuals : MonoBehaviour
{
    private Material mat;
    private int layerDefault;
    private int layerIgnoreRaycast;
    private bool stopFade;
    void Awake()
    {
        layerDefault = LayerMask.NameToLayer("Default");
        layerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.tag = "Editable";

        mat = GetComponent<Renderer>().material;
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    public void MakeObjectTransparent(float transparentValue)
    {
        Color color = mat.color;
        color.a = transparentValue;
        mat.color = color;
    }

    public void RaycastsEnabled()
    {
        gameObject.layer = layerDefault;
    }

    public void RaycastsDisabled()
    {
        gameObject.layer = layerIgnoreRaycast;
    }


}
