using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class PanelSetting : MonoBehaviour
{
    //InputField inputField;
    Button btn_exit;
    Toggle toggle_Rigidbody;
    Toggle toggle_Sound;



    public static PanelSetting instance;
    

    private void Start()
    {
        Init();
        instance = this;
    }
    private void Init()
    {
        //inputField = GetComponentInChildren<InputField>();

        btn_exit = GameObject.Find("Btn_Exit").GetComponent<Button>();
        btn_exit.onClick.AddListener(SetPanelStatus);

        toggle_Sound = GameObject.Find("TG_Sound").GetComponentInChildren<Toggle>();
        toggle_Sound.onValueChanged.AddListener(delegate { ToggleSound(toggle_Sound); });

        toggle_Rigidbody = GameObject.Find("TG_Rigidbody").GetComponentInChildren<Toggle>();
        toggle_Rigidbody.onValueChanged.AddListener(delegate { ToggleRigidbody(toggle_Rigidbody); });

        //set the initial rotation increament;
        SetIncreament();
    }

    public void SetRotationIncreament(string s)
    {
        
        if (s.All(char.IsDigit))
        {
            int increament = Convert.ToInt32(s);
            GameManager.instance.RotateAngle=increament;
        }
       
    }

    void SetPanelStatus()
    {
        AudioManager.instance.PlayUITap();
        
        Setting.instance.HidePanel();
    }

     void SetIncreament()
    {
        InputField inputField = GetComponentInChildren<InputField>();
        int increament = GameManager.instance.RotateAngle;
        inputField.text = increament.ToString();
    }

   void ToggleSound(Toggle toggle)
    {
        AudioManager.instance.ToggleAudio(toggle_Sound.isOn);
        AudioManager.instance.PlayUITap();
    }

    void ToggleRigidbody(Toggle toggle)
    {
        AudioManager.instance.PlayUITap();

        //todo enable rigibody of the building blocks;
        BuildBlock.instance.ToggleRigidbody(toggle.isOn);
    }
}
