using System;
using System.IO;
using UnityEngine;

[Serializable]
public enum Record
{
    //커스터마이징 필요
    None,
    Uncheck,
    Done
}

public enum Indentity
{
    //손님의 정체성
    human,
    vampire,
}



public class Customer {
    public Indentity indentity;
    public string name;
    public Room room; //할당받은 방
    public int satisfy; //만족도
    //단서 배열 

    //손님을 특정 룸에 할당하는 메서드

    //손님이 요청하는 메서드
}

public class Room
{
    Customer customer; //입실한 손님


    
}

[Serializable]
public class GameData{

    #region 1. 호텔
    public int money;
    public int reputation;
    public Room[] rooms;


    #endregion


    public int year;
    public int month;
    public int day;

}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region 1. 능력치
    public int _day = 1;

    public int statMaxValue = 100;
    public int personalityMaxValue = 5;


    #endregion

    #region 2. 시간

    public int day
    {
        get { return _gameData.day; }
        set { _gameData.day = value; }
    }

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
