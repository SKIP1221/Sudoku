using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Toggle SoundToggle;
    public Button ButtonChangeName;
    public Button ButtonExit;

    public MenuController menuController;

    private void Start()
    {
        SoundToggle.onValueChanged.AddListener( state => PlayerPrefs.SetInt("Sound", state ? 1 : 0) );

        ButtonChangeName.onClick.AddListener(() =>
        {
            MenuEvents.ClosePopUp(gameObject);
            MenuEvents.OpenPopUp(menuController.NamePopUp);
        });

        ButtonExit.onClick.AddListener(() =>
        {
            MenuEvents.ClosePopUp(gameObject);
            MenuEvents.OpenPopUp(menuController.ExitPopUp);
        });

        if (PlayerPrefs.HasKey("Sound")) SoundToggle.isOn = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
    }
}
