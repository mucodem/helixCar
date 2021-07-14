using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{

    TextAsset myText;
    string[] rows;

    #region Singleton
    public static CSVReader instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        myText = Resources.Load<TextAsset>("HelixCar");
    }
    #endregion  

    public void CsvFileRead(int level)
    {
        string[] lines = myText.text.Split(' ');
        rows = lines[level].Split(';');
        ValueUpdater();
        Player.instance.speedTx.text = CSVReader.instance.rows[1].ToString();
        Player.instance.maxPointTx.text = CSVReader.instance.rows[3].ToString();
        Debug.Log(level + "-------------------------" + CSVReader.instance.rows[1] + "-------------------------" +
                        CSVReader.instance.rows[2] + "-------------------------"+ CSVReader.instance.rows[3]);
    }

     public void ValueUpdater()
     {
        int.TryParse(rows[0], out Player.instance.level);
        int.TryParse(rows[1], out Player.instance.speed);
        int.TryParse(rows[2], out PathCreator.instance.distance);
        int.TryParse(rows[3], out PathCreator.instance.obsNum);
     }
}