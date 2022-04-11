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

    string[] level = { "00000000000000000000",
                       "00000000000000000000",
                       "00000000111111110000",
                       "00000000100000010000",
                       "000000001000000100000",
                       "00000111100000010000",
                       "00000100000000010000",
                       "00000100000000010000",
                       "00111100000000011111",
                       "11100000000000000000",
                       "00000000000000000000"};

    void Start()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (var i = 0; i < fieldHeight; i++)
            for (var j = 0; j < fieldWidth; j++)
            {
                int spriteIndex = int.Parse(level[i].ToCharArray()[j].ToString());
                Sprite spr = tileSprite[spriteIndex];
                CreateCell(spr, j, i, worldVec);
            }
    }

    void CreateCell(Sprite spr, int x, int y, Vector3 V)
    {
        GameObject tmpCell = Instantiate(cellPref);
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        float spriteSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float spriteSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(V.x + spriteSizeX * x, V.y + spriteSizeY * -y);
    }

    string[] LoadLevelText(int i)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + i + "Ground");

        string tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);

        return tmpStr.Split('!');
    }
}


