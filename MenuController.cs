using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject LevelScreen;
    public GameObject MainMenuScreen;
    public GameObject Blur;

    public GameObject NamePopUp;
    public GameObject SettingsPopUp;
    public GameObject LevelPopUp;
    public GameObject ExitPopUp;

    void Start()
    {
        MenuEvents.closePopUp.AddListener(_object => 
        {
            _object.SetActive(false);
            Blur.SetActive(false);
        });

        MenuEvents.openPopUp.AddListener(_object =>
        {
            _object.SetActive(true);
            Blur.SetActive(true);
        });

        MenuEvents.openScreen.AddListener(_object =>
        {
            _object.SetActive(true);
        });

        if (!PlayerPrefs.HasKey("PlayerName"))
            MenuEvents.OpenPopUp(NamePopUp);
    }


    public void OnClickClose(GameObject @object) => MenuEvents.ClosePopUp(@object);
}
