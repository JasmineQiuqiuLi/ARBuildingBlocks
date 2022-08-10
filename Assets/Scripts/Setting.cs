using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    GameObject panel_Setting;
    Button btn_setting;
    
    bool isShowPanelSetting=false;

    public static Setting instance;

    private void Awake()
    {
        Init();
        instance = this;
    }


    private void Init()
    {
        panel_Setting = GameObject.Find("Panel_Setting");
        btn_setting = gameObject.GetComponent<Button>();
        btn_setting.onClick.AddListener(ToggleSettingPanel);
        panel_Setting.SetActive(false);

    }

    void ToggleSettingPanel()
    {
        
        isShowPanelSetting = !isShowPanelSetting;
        panel_Setting.SetActive(isShowPanelSetting);

        AudioManager.instance.PlayUITap();
    }


    public void HidePanel()
    {
        isShowPanelSetting = false;
        panel_Setting.SetActive(false);
    }
}
