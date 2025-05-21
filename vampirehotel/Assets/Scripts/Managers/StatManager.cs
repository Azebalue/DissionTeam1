using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager 
{
    public int[] studySet = new int[2];
    public float speed = 0.15f;

    public ref float GetStatRef(int statID)
    {
        return ref Managers.Game.stats[statID];
    }

    public void RaiseStat(int statID, float increaseValue)
    {
        //float stat = GetStatRef(statID);
        ref float stat = ref GetStatRef(statID);
        stat += increaseValue;
        if(stat >= PlayerStatData.maxValue)
        {
            Debug.LogWarning($"최대치 도달");
            stat = PlayerStatData.maxValue;
        }
    }
}
