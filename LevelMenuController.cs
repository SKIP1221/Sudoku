using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class LevelMenuController : MonoBehaviour
{
    public List<TextMeshProUGUI> buttons = new List<TextMeshProUGUI>();
    public Color StaticColor;
    public Color nonStaticColor;
    private int pressButtonId;

    [HideInInspector]
    public int level;
    public TextMeshProUGUI LevelText;

    public ProgressBar progressBar;
    private int[] trueAnswers=new int[81];
    private int[] answers = new int[81];

    void Start()
    {
        MenuEvents.clickSudokuButton.AddListener((row, column) =>
        {
            pressButtonId = row * 9 + column;
        });

        MenuEvents.clickSudokuKeyBoardButton.AddListener(num =>
        {
            if (PlayerPrefs.GetInt("Level"+ level + ".button" + (pressButtonId + 1).ToString() + ".isStatic")==0)
            {
                buttons[pressButtonId].color = nonStaticColor;
                buttons[pressButtonId].text = num==0 ? "" : num.ToString();
                PlayerPrefs.SetInt("Level" + level + ".button" + (pressButtonId + 1).ToString(), num);
                answers[pressButtonId] = num;
                UpdateProgressBar();
            }
        });

        LevelText.text = "УРОВЕНЬ: " + level.ToString();
        for (int i = 1; i < 82; i++)
        {
            //заполнение поля
            int text = PlayerPrefs.GetInt("Level" + level + ".button" + i.ToString());
            buttons[i-1].text = text == 0 ? "" : text.ToString();
            buttons[i - 1].color = PlayerPrefs.GetInt("Level" + level + ".button" + i.ToString() + ".isStatic") == 1 ? StaticColor : nonStaticColor;

            //подгрузка ответов
            trueAnswers[i - 1] = PlayerPrefs.GetInt("Level" + level + ".button" + i.ToString() + ".trueValue");
            answers[i-1] = PlayerPrefs.GetInt("Level" + level + ".button" + i.ToString());
            UpdateProgressBar();
        }
    }

    private void UpdateProgressBar()
    {
        int glasses = 0;
        for (int i = 1; i < 82; i++)
            if (PlayerPrefs.GetInt("Level" + level + ".button" + i.ToString() + ".isStatic") == 0)
                if (trueAnswers[i - 1] == answers[i - 1] && answers[i - 1] != 0)
                    glasses++;
        progressBar.ChangeValue(glasses*100/(36 + level * 2));
    }
}
