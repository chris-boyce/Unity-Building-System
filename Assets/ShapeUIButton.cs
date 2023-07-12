using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeUIButton : MonoBehaviour
{
    public Enums.Shapes Shape;
    public BuildTool buildTool;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeShape);
    }

    void ChangeShape()
    {
        switch (Shape)
        {
            case Enums.Shapes.Cube:
                buildTool.ShapeSelector(PrimitiveType.Cube);
                break;
            case Enums.Shapes.Sphere:
                buildTool.ShapeSelector(PrimitiveType.Sphere);
                break;
            case Enums.Shapes.Capsule:
                buildTool.ShapeSelector(PrimitiveType.Capsule);
                break;
            case Enums.Shapes.Null:
                break;

        }
    }

    public void ChangeModel(GameObject prefab)
    {
        buildTool.ModelSelector(prefab);
    }

    
}
