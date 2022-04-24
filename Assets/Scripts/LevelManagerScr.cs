using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManagerScr : MonoBehaviour
{
    public int fieldWidth, fieldHeight;
    public GameObject cellPref;

    public Transform cellParent;

    public Sprite[] tileSprite = new Sprite[2];

    public List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCells = new GameObject[11, 20];

    int currentWayX, currentWayY;
    GameObject firstCell;

    string[] level = { "00100000000000000000",
                       "00100000000000000000",
                       "00111000000000000000",
                       "00001000000000011111",
                       "000010000000000100000",
                       "00001000000000010000",
                       "00001000000000010000",
                       "00001110000000010000",
                       "00000010000000110000",
                       "00000011111111100000",
                       "00000000000000000000"};

    void Start()
    {
        CreateLevel();
        LoadWayPoints();
    }

    void CreateLevel()
    {
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (var i = 0; i < fieldHeight; i++)
            for (var j = 0; j < fieldWidth; j++)
            {
                int spriteIndex = int.Parse(level[i].ToCharArray()[j].ToString());
                Sprite spr = tileSprite[spriteIndex];
                bool isGround = spr == tileSprite[1] ? true : false;
                CreateCell(isGround, spr, j, i, worldVec);
            }
    }

    void CreateCell(bool isGround, Sprite spr, int x, int y, Vector3 V)
    {
        GameObject tmpCell = Instantiate(cellPref);
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        float spriteSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float spriteSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(V.x + (spriteSizeX * x), V.y + (spriteSizeY * -y));

        if (isGround)
        {
            tmpCell.GetComponent<CellScr>().isGround = true;
            if (firstCell == null)
            {
                firstCell = tmpCell;
                currentWayX = x;
                currentWayY = y;
            }
        }

        allCells[y, x] = tmpCell;
    }

    string[] LoadLevelText(int i)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + i + "Ground");

        string tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);

        return tmpStr.Split('!');
    }

    void LoadWayPoints()
    {
        GameObject currentWayGo;
        wayPoints.Add(firstCell);

        while(true)
        {
            currentWayGo = null;
            if (currentWayX > 0 && allCells[currentWayY, currentWayX - 1].GetComponent<CellScr>().isGround && !wayPoints.Exists(x => x == allCells[currentWayY, currentWayX - 1]))
            {
                currentWayGo = allCells[currentWayY, currentWayX - 1];
                currentWayX--;
            }

            else if (currentWayX < (fieldWidth - 1) && allCells[currentWayY, currentWayX + 1].GetComponent<CellScr>().isGround && !wayPoints.Exists(x => x == allCells[currentWayY, currentWayX + 1]))
            {
                currentWayGo = allCells[currentWayY, currentWayX + 1];
                currentWayX++;
            }

            else if (currentWayY > 0 && allCells[currentWayY - 1, currentWayX].GetComponent<CellScr>().isGround && !wayPoints.Exists(x => x == allCells[currentWayY - 1, currentWayX]))
            {
                currentWayGo = allCells[currentWayY - 1, currentWayX];
                currentWayY--;
            }

            else if (currentWayY < (fieldHeight - 1) && allCells[currentWayY + 1, currentWayX].GetComponent<CellScr>().isGround && !wayPoints.Exists(x => x == allCells[currentWayY + 1, currentWayX]))
            {
                currentWayGo = allCells[currentWayY + 1, currentWayX];
                currentWayY++;
            }
            else
                break;
            wayPoints.Add(currentWayGo);
        }
    }
}


