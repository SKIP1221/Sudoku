using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPopUpController : MonoBehaviour
{
    [System.Serializable]
    public class Label
    {
        public TextMeshProUGUI[] button = new TextMeshProUGUI[9];
    }
    public List<Label> zone = new List<Label>();
    private int level = 0;

    public Button resetButton;
    public Button startButton;
    public MenuController menuController;

    private void Start()
    {
        resetButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Sudoku.DeleteZone(level);
            Sudoku.CreateZone(level);
            MenuEvents.ClosePopUp(gameObject);
        });

        startButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            GameObject levelScreen= menuController.LevelScreen;
            MenuEvents.OpenScreen(levelScreen);
            levelScreen.GetComponent<LevelMenuController>().level = level;
            MenuEvents.ClosePopUp(gameObject);
        });
    }

    public void LoadZone(int levelNum)
    {
        level = levelNum;
        if (PlayerPrefs.HasKey("Level"+levelNum+".isUsed"))
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    int buttonNum = y * 9 + (x + 1);
                    int val = PlayerPrefs.GetInt("Level"+levelNum+".button"+buttonNum.ToString());
                    zone[y].button[x].text = val != 0 ? val.ToString() : "";
                }
            }
        }
    }
}
