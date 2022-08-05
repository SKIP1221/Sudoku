using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuKeyBoardController : MonoBehaviour
{
    public void OnClickKeyBoardButton(int num) => MenuEvents.ClickSudokuKeyBoardButton(num);
}
