using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
        

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private RectTransform texture;
    [SerializeField] private Texture2D refSprite;

    [SerializeField] private EditTool editTool;
    [SerializeField] private Image buttonImage;
    private Color PickedColor;

    public void OnClick()
    {
        PickColor();
    }

    private void PickColor()
    {
        Vector3 imagePos = texture.position;
        float globalposX = Input.mousePosition.x - imagePos.x;
        float globalposY = Input.mousePosition.y - imagePos.y;
        
        int localPosX = (int)(globalposX*(refSprite.width/texture.rect.width));
        int localPosY = (int)(globalposY * (refSprite.height / texture.rect.height));

        PickedColor = refSprite.GetPixel(localPosX, localPosY);
        editTool.selectedColor = PickedColor;
        buttonImage.color = PickedColor;
    }
}
