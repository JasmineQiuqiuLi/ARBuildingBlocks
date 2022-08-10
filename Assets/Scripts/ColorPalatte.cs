using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalatte : MonoBehaviour
{
    GameObject palatte;
    public static ColorPalatte instance;
    private void Awake()
    {
        palatte = GameObject.Find("Palette");
        SetPalatteStatus(false);

        instance = this;
    }

    public void SetPalatteStatus(bool status)
    {
        palatte.SetActive(status);
    }

}
