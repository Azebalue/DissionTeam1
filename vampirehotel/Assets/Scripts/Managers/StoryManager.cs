using UnityEngine;
using System;

public class StoryManager
{

    public void Init()
    {
        curID = startID;
    }


    public int startID = 1001;
    public int curID; //현재 dialogueData
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
                Debug.LogWarning($"스토리 데이터[{curID}]가 없어서 임시 스토리[1001] 적용함 \n {e}");
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

    //스토리의 마지막까지 가면 false 반환
    public bool NextLine()
    {
        didBranchStart = false; //분기 스토리 시작한건지 체크(지우지 마라 ㄹㅇ...)
        //분기가 안갈리는 경우
        if (hasBranched == false)
        {
            ++curID;
            if (didStoryEnd == true)
            {
                //분기 스토리 종료
                if (isBranch == true)
                {
                    isBranch = false; //브랜치 탈출
                    curID = lastID + 1;
                }
                //일반 스토리 종료
                else
                {
                    Debug.LogWarning($"스토리 종료 : {curID}");
                    return false;
                }

            }


        }
        //분기 발생한 경우
        else
        {
            lastID = curID;
            BranchEffect();
            ++curID;
            Debug.LogWarning($"분기 발생 : {lastID}");
            isBranch = true; //브랜치 진입
            didBranchStart = true; //브랜치 진입했으니까 판정 UI띄우라고 조건 변경해준거임

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
            Debug.Log($"{Managers.Game.stats[statID]} 성공: {statValue} >= {Managers.Data.Branches[Line.branchID].standard}");

            effect =  Managers.Data.Branches[Line.branchID].trueEffect;
            
        }
        else
        {
            Debug.Log($"{Managers.Game.stats[statID]} 실패: {statValue} < {Managers.Data.Branches[Line.branchID].standard}");

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
