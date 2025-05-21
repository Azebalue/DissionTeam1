using System;
using System.Collections.Generic;

//데이터는 json에 있을 뿐, 여기는 데이터 X, just for data container
[System.Serializable]
public class StartData 
{
    public int month;
    public int year;

    public float startValue;

}

/*[System.Serializable]
public class StartDataLoader : ILoader<int, StartData>
{
    public List<StartData> data = new List<StartData>();

    public Dictionary<int, StartData> MakeDict()
    {
        Dictionary<int, StartData> dic = new Dictionary<int, StartData>();

        foreach (StartData _data in data)
            dic.Add(_data.ID, _data);

        return dic;
    }
}*/