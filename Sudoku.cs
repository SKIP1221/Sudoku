using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class Sudoku : MonoBehaviour
{
    public static void CreateZone(int difficulte)
    {
        int[,] zone = {
            {1,2,3,4,5,6,7,8,9},
            {4,5,6,7,8,9,1,2,3},
            {7,8,9,1,2,3,4,5,6},
            {2,3,4,5,6,7,8,9,1},
            {5,6,7,8,9,1,2,3,4},
            {8,9,1,2,3,4,5,6,7},
            {3,4,5,6,7,8,9,1,2},
            {6,7,8,9,1,2,3,4,5},
            {9,1,2,3,4,5,6,7,8}
        };

        mix(ref zone, 50);

        saveTrueValues(zone, difficulte);

        clearZone(ref zone, difficulte);

        saveGame(zone, difficulte);
    }

    public static void DeleteZone(int difficulte)
    {
        PlayerPrefs.DeleteKey("Level" + difficulte + ".isUsed");
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                int num = y * 9 + x + 1;
                PlayerPrefs.DeleteKey("Level" + difficulte + ".button" + num);
                PlayerPrefs.DeleteKey("Level" + difficulte + ".button" + num + ".isStatic");
                PlayerPrefs.DeleteKey("Level" + difficulte + ".button" + num + ".trueValue");
            }
        }
    }

    private static void saveTrueValues(int[,] zone, int difficulte)
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                int num = y * 9 + x + 1;
                PlayerPrefs.SetInt("Level" + difficulte + ".button" + num + ".trueValue", zone[y, x]);
            }
        }
    }

    private static void saveGame(int[,] zone, int difficulte)
    {
        PlayerPrefs.SetInt("Level" + difficulte + ".isUsed", 1);
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                int num = y * 9 + x + 1;
                PlayerPrefs.SetInt("Level" + difficulte + ".button" + num, zone[y, x]);
                PlayerPrefs.SetInt("Level" + difficulte + ".button" + num + ".isStatic", zone[y, x] != 0 ? 1 : 0);
            }
        }
    }

    private static void mix(ref int[,] zone, int blur)
    {
        for (int i = 0; i < blur; i++)
        {
            int methodNum = Random.Range(0, 5);

            switch (methodNum)
            {
                case 0:
                    transposing(ref zone);
                    break;
                case 1:
                    swap_rows_small(ref zone);
                    break;
                case 2:
                    swap_colums_small(ref zone);
                    break;
                case 3:
                    swap_rows_area(ref zone);
                    break;
                case 4:
                    swap_colums_area(ref zone);
                    break;
            }
        }

    }

    private static void transposing(ref int[,] zone)
    {
        int[,] bufferZone = new int[zone.GetLength(0), zone.GetLength(1)];
        System.Array.Copy(zone, bufferZone, zone.Length);

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                zone[y, x] = bufferZone[x, y];
            }
        }
    }

    private static void swap_rows_small(ref int[,] zone)
    {
        int area = Random.Range(0, 3);
        int line1 = Random.Range(0, 3);
        int line2 = Random.Range(0, 3);
        while (line1==line2)
            line2 = Random.Range(0, 3);

        int[,] bufferZone = new int[zone.GetLength(0), zone.GetLength(1)];
        System.Array.Copy(zone, bufferZone, zone.Length);

        for (int i = 0; i < 9; i++)
        {
            zone[area * 3 + line1, i] = bufferZone[area * 3 + line2, i];
            zone[area * 3 + line2, i] = bufferZone[area * 3 + line1, i];
        }
    }

    private static void swap_colums_small(ref int[,] zone)
    {
        transposing(ref zone);
        swap_rows_small(ref zone);
        transposing(ref zone);
    }

    private static void swap_rows_area(ref int[,] zone)
    {
        int[,] bufferZone = new int[zone.GetLength(0), zone.GetLength(1)];
        System.Array.Copy(zone, bufferZone, zone.Length);

        int area1 = Random.Range(0, 3);
        int area2 = Random.Range(0, 3);
        while (area1 == area2)
            area2 = Random.Range(0, 3);
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                zone[area1 * 3 + y, x] = bufferZone[area2 * 3 + y, x];
                zone[area2 * 3 + y, x] = bufferZone[area1 * 3 + y, x];
            }
        }
    }

    private static void swap_colums_area(ref int[,] zone)
    {
        transposing(ref zone);
        swap_rows_area(ref zone);
        transposing(ref zone);
    }

    private static void clearZone(ref int[,] zone,int difficulte)
    {
        difficulte *= 2;
        difficulte+= 36;

        while (difficulte>0)
        {
            int x = Random.Range(0,9);
            int y = Random.Range(0,9);
            if (zone[y, x] != 0)
            {
                int buffer = zone[y, x];
                zone[y, x] = 0;

                if (Solve(zone))
                    difficulte--;
                else
                    zone[y, x] = buffer;
            }
        }
    }

    public static bool Solve(int[,] zone)
    {
        int[,] sudoku = new int[zone.GetLength(0), zone.GetLength(1)];
        System.Array.Copy(zone, sudoku, zone.Length);
        bool isChanged = true;
        bool isFull = false;
        while (isChanged)
        {
            isChanged = false;
            isFull = true;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (sudoku[y, x] == 0)
                    {
                        isFull = false;
                        var line = getRowFixedNumbers(y, sudoku);
                        var col = getColumnFixedNumbers(x, sudoku);
                        var block = getBlockFixedNumbers(y, x, sudoku);

                        var variants = Enumerable.Range(1, 9).Except(line).Except(col).Except(block);
                        if (variants.Count() == 1)
                        {
                            sudoku[y, x] = variants.First();
                            isChanged = true;
                        }
                    }
                }
            }
        }
        return isFull;
    }

    private static IEnumerable<int> getRowFixedNumbers(int rowIndex, int[,] sudoku)
    {
        for (int x = 0; x < 9; x++)
        {
            if (sudoku[rowIndex, x] != 0) yield return sudoku[rowIndex, x];
        }
    }

    private static IEnumerable<int> getColumnFixedNumbers(int columnIndex, int[,] sudoku)
    {
        for (int y = 0; y < 9; y++)
        {
            if (sudoku[y, columnIndex] != 0) yield return sudoku[y, columnIndex];
        }
    }

    private static IEnumerable<int> getBlockFixedNumbers(int y, int x, int[,] sudoku)
    {
        int startY = 3 * (y / 3);
        int startX = 3 * (x / 3);
        for (int yPos = 0; yPos < 3; yPos++)
        {
            for (int xPos = 0; xPos < 3; xPos++)
            {
                if (sudoku[yPos + startY, xPos + startX] != 0)
                    yield return sudoku[yPos + startY, xPos + startX];
            }
        }
    }
}
