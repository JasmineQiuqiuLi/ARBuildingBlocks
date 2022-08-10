using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Shape
{
    Sphere,
    Cube,
    Cylinder,
    Cone
}
public class ShapeButton : MonoBehaviour
{
    public Shape shape;
    Color highlighted = new Color(0, 210, 255, 255);
    Color defaultColor = Color.white;
    ShapeButton[] shapeButtons;

    private void Awake()
    {
        shapeButtons = transform.parent.GetComponentsInChildren<ShapeButton>();
    }

    public void SetCurShape()
    {
       
        ShapeMenu.instance.SetCurShape(shape);
        SetSelectedIconColor();

        //play sound effects
        AudioManager.instance.PlayUITap();
    }

     void SetSelectedIconColor()
    {
        foreach(var shapeButton in shapeButtons)
        {
            shapeButton.SetColor(defaultColor);
        }

        SetColor(highlighted);
    }

    void SetColor(Color color)
    {
        Image image = GetComponentInChildren<Image>();
        image.color = color;
    }


}
