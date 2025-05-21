using JetBrains.Annotations;
using System;
using System.Collections.Generic;

#region �����͸��
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
    public int storyID; //���丮 ���� ���̵�
    //public List<int> dialogueIDs;
    public int startID; //���丮�� ���� ��� ��ȣ
    public int endID; //... ���� ��� ��ȣ


    public bool hasBranch;
    public int successStoryID; //���� �б� ��� ����
    public int failureStoryID; //���� ��� �б� ����
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
