using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NamePopUpController : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public GameObject ButtonClosePopUp;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
            ButtonClosePopUp.SetActive(false);
    }


    public void OnClickEnter()
    {
        if (NameInputField.text!="")
        {
            PlayerPrefs.SetString("PlayerName", NameInputField.text);
            MenuEvents.ClosePopUp(gameObject);
            MenuEvents.ChangeName(NameInputField.text);
            NameInputField.text = "";
            if (!ButtonClosePopUp.activeSelf)
            {
                ButtonClosePopUp.SetActive(true);
            }
        }
    }
}
