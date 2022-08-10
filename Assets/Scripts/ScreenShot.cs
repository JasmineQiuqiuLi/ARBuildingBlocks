using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenShot : MonoBehaviour
{
    
    public void TakeScreenshot()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss");
        string fileName = "Screenshot" + timeStamp+".png";
        ScreenCapture.CaptureScreenshot(fileName);

        //play sound effects
        AudioManager.instance.PlayUITap();

    }
}
