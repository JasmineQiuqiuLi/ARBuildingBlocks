using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShapeMenu : MonoBehaviour
{
    public Shape curShape { get; private set; }
   // public Shape CurShape { get { return curShape; } }

    public static ShapeMenu instance;

    private void Awake()
    {
        instance = this;
        curShape = Shape.Cube;
    }

    public void SetCurShape(Shape shape)
    {
        curShape = shape;
    }
}
