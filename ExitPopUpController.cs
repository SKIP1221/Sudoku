using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPopUpController : MonoBehaviour
{
    public Button ExitButton;
    public Button CancelButton;

    void Start()
    {
        ExitButton.onClick.AddListener( () => Application.Quit() );
        CancelButton.onClick.AddListener( () => MenuEvents.ClosePopUp(gameObject) );
    }
}
