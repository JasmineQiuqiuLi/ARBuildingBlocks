using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    GameObject Menu_Operation;
    public static UIController instance;

    private void Awake()
    {
        Menu_Operation = GameObject.Find("TransformMenu");
        Menu_Operation.SetActive(false);
        instance = this;
    }

    public void ToggleMenu(bool status)
    {
        Menu_Operation.SetActive(status);
    }
}
