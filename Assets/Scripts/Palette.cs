using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palette : MonoBehaviour
{
    public Color[] colors;
    public GameObject colorPrefab;

    private void Awake()
    {
        InitPalette();
    }

    void InitPalette()
    {
        GameObject palette = GameObject.Find("Palette");
        for(int i = 0; i < colors.Length; i++)
        {
            //set the button icon color
            GameObject colorObject=Instantiate(colorPrefab, palette.transform);
            colorObject.GetComponentInChildren<Image>().color = colors[i];

            //set the color property of each ChangeColor component
            ChangeColor changeColor = colorObject.GetComponent<ChangeColor>();
            changeColor.SetColor(colors[i]);

            //set the position of each color button
            colorObject.GetComponent<RectTransform>().localPosition = new Vector3(i * (135), 0, 0); 
        }
        //set the size of the parent.
        palette.GetComponent<RectTransform>().sizeDelta = new Vector2(colors.Length * 135 + 10, 120);
    }
}
