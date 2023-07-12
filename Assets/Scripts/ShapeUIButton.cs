using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShapeUIButton : MonoBehaviour
{
    [SerializeField] private Enums.Shapes Shape;
    [SerializeField] private BuildTool buildTool;
    [SerializeField] private AudioClip sound_UIClick;
    void OnEnable()
    {
        if (sound_UIClick != null)
            SoundManager.instance?.PlaySoundEffect(sound_UIClick);
    }
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
        Shape = Enums.Shapes.Null;
    }

    
}
