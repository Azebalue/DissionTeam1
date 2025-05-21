using System;
using System.IO;
using UnityEngine;
using static Define;

[Serializable]
public enum Record
{
    //Ŀ���͸���¡ �ʿ�
    None,
    Uncheck,
    Done
}

[Serializable]
public class GameData{

    #region 1. ȣ��
    public int day;
    public float money;

    #endregion


    public int year;
    public int month;

    public int MaxMonth;

/*    //Ŭ������ ���� 
    public Record[] endings = new Record[MAX_ENDING_COUNT];*/
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region 1. �ɷ�ġ
    public int day = 1;


    public int statMaxValue = 100;
    public int personalityMaxValue = 5;
   

    #endregion

    #region 2. �ð�
    public int month
    {
        get { return _gameData.month; }
        set { _gameData.month = value; }
    }
    public int year
    {
        get { return _gameData.year; }
        set { _gameData.year = value; }
    }

    public int MaxMonth
    {
        get { return _gameData.MaxMonth; }
        set { _gameData.MaxMonth = value; }
    }


    #endregion

    #region 4. ��Ÿ
    public static int maxStat
    {
        get { return maxStat; }
        set { maxStat = value; }
    }
    #endregion

    public void Init()
    {
        StartData startData = Managers.Data.Start;

        
        if(startData == null )
        {
            Debug.Log("[GameManagerEx] StartData�� �ε���� �ʾҽ��ϴ�!");
            return;
        }

        year = startData.year;
        month = startData.month;
        MaxMonth = 12 * 6;
    }

    #region Save&Load

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        if (!File.Exists(Managers.savePath))
        {
            Debug.Log($"���: {Managers.savePath}");
            Debug.LogWarning("[WARNING] ������ �������� ����. �⺻ ������ ���� ��...");
            string defaultJson = "{}"; // �⺻ JSON ��
            File.WriteAllText(Managers.savePath, defaultJson);
        }

        File.WriteAllText(Managers.savePath, jsonStr);
        Debug.Log($"Save Game Completed : {Managers.savePath}");

    }

    public bool LoadGame()
    {
        //�ε��ߴ� ���� ������ �ִ��� ��ȯ��
        if (File.Exists(Managers.savePath) == false)
            return false;

        //�����ߴ� ������ ������
        string fileStr = File.ReadAllText(Managers.savePath);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if(data != null )
        {
            Managers.Game.SaveData = data;
        }

        Debug.Log($"Save Game Loaded : {Managers.savePath}");
        return true;
    }
    #endregion
}
