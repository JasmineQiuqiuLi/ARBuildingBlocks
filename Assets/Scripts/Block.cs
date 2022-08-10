using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool isSelected;

    Color lastColor = Color.white;

   
    public Shape shape;

    private void Awake()
    {
        isSelected = false;

    }

    public void SetStatus(bool status)
    {
        isSelected = status;
    }

    public void SetSelectedMaterial()
    {
        MeshRenderer meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        Color curColor = meshRenderer.material.color;

        //Color.cyan is the highlighted color indicating an object is currently selected.
        //last color will be set to an object's material after the object is deselected;
        if (curColor != Color.cyan)
        {
            lastColor = curColor;
        }

        meshRenderer.material.color = Color.cyan;

    }

    public void SetDeselectedColor()
    {
        
        MeshRenderer meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

        meshRenderer.material.color = lastColor;
    }

    public void Rotate(float angle)
    {
        //the mechanic: the user swipe upward to rotate an object clockwise, and downward to the opsite.
        //the angle between the start position and end position should be less than 45 degree (clockwise) and more than 145 degree (counterclockwise)
        Vector3 rotation;
        if (angle < 45)
        {
            //increase the angle;
            rotation = SetRotateAngle();
        }

        else if (angle > 135)
        {
            //decrease the angle;
           rotation = -SetRotateAngle();

        }
        else
        {
            rotation = Vector3.zero;
        }
        transform.eulerAngles += rotation;

        AudioManager.instance.PlayManipulationSound();
    }

    Vector3 SetRotateAngle()
    {

        Vector3 rotAngle;

        switch (MenuOpration.curAxis)
        {
            case Axis.X:
                rotAngle = new Vector3(GameManager.instance.RotateAngle, 0, 0);
                break;
            case Axis.Y:
                rotAngle = new Vector3(0,GameManager.instance.RotateAngle, 0);
                break;
            case Axis.Z:
                rotAngle = new Vector3(0,0,GameManager.instance.RotateAngle);
                break;
            default:
                rotAngle = new Vector3(GameManager.instance.RotateAngle, 0, 0);
                break;
        }
        return rotAngle;
    }

    public void SetGameObjectColor(Color color)
    {
        MeshRenderer msRenderer = GetComponentInChildren<MeshRenderer>();
        msRenderer.material.color = color;
        lastColor = color;
    }

    public void SetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    public void SetPosition(Vector3 newPosDelta)
    {
        transform.position += newPosDelta;
    }


}
