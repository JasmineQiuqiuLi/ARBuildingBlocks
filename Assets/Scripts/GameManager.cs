using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int angle = 10;
   
    public int RotateAngle { get { return angle; } set { angle = value; } }

    public static GameManager instance;

   // GameObject Menu_Operation;

    private void Awake()
    {
        instance = this;
       
    }

    


}
