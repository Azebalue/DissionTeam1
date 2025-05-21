using System;
using System.IO;
using UnityEngine;
using static Define;

[Serializable]
public enum Record
{
    //커스터마이징 필요
    None,
    Uncheck,
    Done
}

[Serializable]
public class GameData{

    #region 1. 호텔
    public int day;
    public float money;

    #endregion


    public int year;
    public int month;

    public int MaxMonth;

/*    //클리어한 엔딩 
    public Record[] endings = new Record[MAX_ENDING_COUNT];*/
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region 1. 능력치
    public int day = 1;


    public int statMaxValue = 100;
    public int personalityMaxValue = 5;
   

    #endregion

    #region 2. 시간
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

    #region 4. 기타
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
            Debug.Log("[GameManagerEx] StartData가 로드되지 않았습니다!");
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
            Debug.Log($"경로: {Managers.savePath}");
            Debug.LogWarning("[WARNING] 파일이 존재하지 않음. 기본 데이터 생성 중...");
            string defaultJson = "{}"; // 기본 JSON 값
            File.WriteAllText(Managers.savePath, defaultJson);
        }

        File.WriteAllText(Managers.savePath, jsonStr);
        Debug.Log($"Save Game Completed : {Managers.savePath}");

    }

    public bool LoadGame()
    {
        //로드했던 파일 유무가 있는지 반환함
        if (File.Exists(Managers.savePath) == false)
            return false;

        //저장했던 파일이 있으면
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
