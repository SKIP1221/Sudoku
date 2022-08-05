using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainScreenController : MonoBehaviour
{
    [System.Serializable] public class levelStars { public GameObject[] star = new GameObject[3]; }

    public TextMeshProUGUI NameText;
    public GameObject SettingsPopUp;
    public GameObject LevelPopUp;

    public List<levelStars> stars= new List<levelStars>();
    public Color CompliteStarColor;

    private void Start()
    {
        //изменение ника
        MenuEvents.changeName.AddListener(name =>
        {
            NameText.text = name;
            NameText.gameObject.GetComponent<Animator>().Play("NickChange");
        });
        NameText.text = PlayerPrefs.GetString("PlayerName");

        //генерация уровней 
        for (int i = 1; i < 11; i++)
        {
            if (!PlayerPrefs.HasKey("Level"+i+".isUsed"))
            {
                Sudoku.CreateZone(i);
            }
        }
        //выставление звезд
        for (int i = 1; i < 11; i++)
        {
            if (PlayerPrefs.HasKey("Level"+i+".stars"))
            {
                int starCount = PlayerPrefs.GetInt("Level" + i + ".stars");

                //закрашивание выполненных звезд
                for (int x = 0; x < starCount; x++)
                    stars[i-1].star[x].GetComponent<Image>().color = CompliteStarColor;
            }
        }
    }

    public void OnClickSettings() => MenuEvents.OpenPopUp(SettingsPopUp);

    public void OnClickLevelButton(int num)
    {
        MenuEvents.OpenPopUp(LevelPopUp);
        LevelPopUp.GetComponent<LevelPopUpController>().LoadZone(num);
    }
}
