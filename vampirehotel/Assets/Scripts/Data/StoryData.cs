using JetBrains.Annotations;
using System;
using System.Collections.Generic;

#region 데이터목록
[System.Serializable]
public class DialogueData
{
    public int ID; 
    public string characterStr;
    [NonSerialized] 
    public _CharacterData speaker;

    public string background;
    public int branchID;
    public string kor;
    public string eng;
    public string chn;
    public string jpn;

}

//[System.Serializable]
/*public class StoryData
{
    public int storyID; //스토리 고유 아이디
    //public List<int> dialogueIDs;
    public int startID; //스토리의 시작 대사 번호
    public int endID; //... 종료 대사 번호


    public bool hasBranch;
    public int successStoryID; //성공 분기 대사 집합
    public int failureStoryID; //실패 대사 분기 집합
}*/
#endregion

[System.Serializable]
public class DialogueDataLoader : ILoader<int, DialogueData>
{

    public List<DialogueData> data;

    public Dictionary<int, DialogueData> MakeDict()
    {
        Dictionary<int, DialogueData> dict = new Dictionary<int, DialogueData>();
        foreach (DialogueData story in data)
            dict.Add(story.ID, story);

        return dict;
    }

}
