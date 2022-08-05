using UnityEngine.Events;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    public static UnityEvent<GameObject> closePopUp = new UnityEvent<GameObject>();
    public static void ClosePopUp(GameObject _object) => closePopUp.Invoke(_object);


    public static UnityEvent<GameObject> openPopUp = new UnityEvent<GameObject>();
    public static void OpenPopUp(GameObject _object) => openPopUp.Invoke(_object);


    public static UnityEvent<GameObject> openScreen = new UnityEvent<GameObject>();
    public static void OpenScreen(GameObject _object) => openScreen.Invoke(_object);


    public static UnityEvent<GameObject> closeScreen = new UnityEvent<GameObject>();
    public static void CloseScreen(GameObject _object) => closeScreen.Invoke(_object);


    public static UnityEvent<string> changeName = new UnityEvent<string>();
    public static void ChangeName(string nick) => changeName.Invoke(nick);


    public static UnityEvent<int,int> clickSudokuButton = new UnityEvent<int, int>();
    public static void ClickSudokuButton(int row, int column) => clickSudokuButton.Invoke(row, column);


    public static UnityEvent<int> clickSudokuKeyBoardButton = new UnityEvent<int>();
    public static void ClickSudokuKeyBoardButton(int num) => clickSudokuKeyBoardButton.Invoke(num);

}
