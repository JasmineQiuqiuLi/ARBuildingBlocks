using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    Color color;
    public void ChangeObjectColor()
    {
        GameObject curSeletedObject = Touch.instance.CurSelected;
        if (curSeletedObject != null)
        {
            curSeletedObject.GetComponent<Block>().SetGameObjectColor(color);
        }

        //play sound effects
        AudioManager.instance.PlayUITap();
        
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}
