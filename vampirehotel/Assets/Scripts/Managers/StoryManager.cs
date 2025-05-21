using UnityEngine;
using System;

public class StoryManager
{

    public void Init()
    {
        curID = startID;
    }


    public int startID = 1001;
    public int curID; //���� dialogueData
    private int _lastID;
    //private DialogueData _line; 
    public bool didBranchStart;
    
    private int lastID
    {
        get { return _lastID; }
        set { _lastID = value; }
    }
    public DialogueData Line
    {
        get {

            try
            {
                return Managers.Data.Stories[curID];
            }
            catch (Exception e)
            {
                Debug.LogWarning($"���丮 ������[{curID}]�� ��� �ӽ� ���丮[1001] ������ \n {e}");
                curID = 1001;
                return Managers.Data.Stories[curID];
            }

        }

    }

    private bool hasBranched
    {
        get { return (Line.branchID != 0); }
    }
    private bool isBranch = false;

    bool didSuccess;

    private bool didStoryEnd
    {
        get { return (Managers.Data.Stories.ContainsKey(curID) == false);  }
    }

    //���丮�� ���������� ���� false ��ȯ
    public bool NextLine()
    {
        didBranchStart = false; //�б� ���丮 �����Ѱ��� üũ(������ ���� ����...)
        //�бⰡ �Ȱ����� ���
        if (hasBranched == false)
        {
            ++curID;
            if (didStoryEnd == true)
            {
                //�б� ���丮 ����
                if (isBranch == true)
                {
                    isBranch = false; //�귣ġ Ż��
                    curID = lastID + 1;
                }
                //�Ϲ� ���丮 ����
                else
                {
                    Debug.LogWarning($"���丮 ���� : {curID}");
                    return false;
                }

            }


        }
        //�б� �߻��� ���
        else
        {
            lastID = curID;
            BranchEffect();
            ++curID;
            Debug.LogWarning($"�б� �߻� : {lastID}");
            isBranch = true; //�귣ġ ����
            didBranchStart = true; //�귣ġ ���������ϱ� ���� UI����� ���� �������ذ���

        }

        return true;
    }

    private void BranchEffect()
    {
/*        int statID = Managers.Data.Branches[Line.branchID].standardStat;
        float statValue = Managers.Game.stats[statID];
        didSuccess = (statValue >= Managers.Data.Branches[Line.branchID].standard);
        EffectData effect;

        if (didSuccess == true)
        {
            Debug.Log($"{Managers.Game.stats[statID]} ����: {statValue} >= {Managers.Data.Branches[Line.branchID].standard}");

            effect =  Managers.Data.Branches[Line.branchID].trueEffect;
            
        }
        else
        {
            Debug.Log($"{Managers.Game.stats[statID]} ����: {statValue} < {Managers.Data.Branches[Line.branchID].standard}");

            effect =  Managers.Data.Branches[Line.branchID].falseEffect;

        }

        applyEffect(effect);*/

    }

/*    private void applyEffect(EffectData effect)
    {
        curID = effect.nextDialogue;
        startID = effect.nextStory;
        Debug.Log($"{startID}: {effect.nextStory}");
        int stat1= effect.stat1;

        UI_BranchNotice.SetInfo(stat1, Managers.Game.stats[stat1], effect.effect1, didSuccess);

        if (stat1 <= (int)Define.stat.yeoksahak)
        {
            Managers.Game.stats[stat1] += effect.effect1;
        }


    }*/

}
