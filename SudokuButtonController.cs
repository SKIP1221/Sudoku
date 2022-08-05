using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuButtonController : MonoBehaviour
{
    int row = 0;
    int column = 0;

    void Start()
    {
        row = (int.Parse(gameObject.name)-1) / 9;
        column = int.Parse(gameObject.name) - row*9 - 1;

        GetComponent<Button>().onClick.AddListener(() => MenuEvents.ClickSudokuButton(row,column));
        MenuEvents.clickSudokuButton.AddListener((row1, column1) =>
        {

            //раскрашивание клеток
            if (row1==row || column1==column || (row1/3==row/3 && column1/3 == column/3))
            {
                Color col = Color.white;
                col.r = row1==row && column1==column ? 0.5f : 0.85f;
                gameObject.GetComponent<Image>().color = col;
            }
            else
                gameObject.GetComponent<Image>().color = Color.white;

        });
    }
}
