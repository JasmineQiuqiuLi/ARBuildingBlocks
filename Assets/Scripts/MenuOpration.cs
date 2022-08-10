using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Operation
{
    Color,
    Scale,
    Rotate,
    Move,
}

public enum Axis
{
    X,
    Y,
    Z
}

public class MenuOpration : MonoBehaviour
{
    public static Operation curOperation = Operation.Move;

   
    //the property of current script;
    public Operation operation;

    //current rotation axis;
   public static Axis curAxis;

   Axis[] axisArray= { Axis.X,Axis.Y,Axis.Z};

   GameObject rotationIconText;

    Color defaultColor = Color.white;
    Color highLighted = new Color(0, 210, 255, 255);






    public void ChangeOperation()
    {

        if (operation == Operation.Rotate && curOperation!=operation)
        {

            curAxis = Axis.X;
            ChangeIcon(curAxis);
           
        }
        else if (operation == Operation.Rotate && curOperation == operation)
        {

            int index = System.Array.IndexOf(axisArray, curAxis);
            index++;
            if (index == axisArray.Length) index = 0;
            curAxis = axisArray[index];
            ChangeIcon(curAxis);

        }

        if (curOperation == Operation.Color)
        {
            GameObject palatte = GameObject.Find("Palette");
            if (palatte!=null)
            {
                ColorPalatte.instance.SetPalatteStatus(false);
               
            }
        }


        curOperation = operation;

        Touch.instance.SetOperation(operation);

        if (curOperation == Operation.Color)
        {
            ColorPalatte.instance.SetPalatteStatus(true);
        }

        AudioManager.instance.PlayUITap();

        ChangeIconColor();


    }

    void ChangeIcon(Axis axis)
    {
        rotationIconText = GameObject.Find("Text_Axis");
        rotationIconText.GetComponent<TextMeshProUGUI>().text = axis.ToString();
    }


    public void SetIconColor(Color color)
    {
        Image image = GetComponentInChildren<Image>();
        image.color = color;

    }

    void ChangeIconColor()
    {
        MenuOpration[] menuOperations = GameObject.Find("TransformMenu").GetComponentsInChildren<MenuOpration>();

        foreach(var menuOperation in menuOperations)
        {
            menuOperation.SetIconColor(Color.white);
        }

        if (curOperation == operation)
        {
            SetIconColor(highLighted);
        }
    }

     
}
