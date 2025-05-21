using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Threading;

public interface ILoader<Key, Value>
{
    //Ű�� ���� ���� Dictionary�� ��ȯ�ϴ� �޼���
    Dictionary<Key, Value> MakeDict();

}

public class DataManager  
{
    static private SOContainer so;

    private const string googleSheetsBaseUrl =
        "https://script.google.com/macros/s/AKfycbxEiyQLkjWDytu2-BhtvFv920zSm8Iob9xuks4UEcQurm11md5y07GFGfVaqHxNI1CSxQ/exec";

    public StartData Start { get; private set; }
    public Dictionary<int, UITextData> UITexts { get; private set; } = new Dictionary<int, UITextData>();
    public Dictionary<int, PlayerStatData> PlayerStats { get; private set; } = new Dictionary<int, PlayerStatData>();

    public Dictionary<string, _CharacterData> Characters { get; private set; } = new Dictionary<string, _CharacterData>();
    public Dictionary<int, CharacterTextData> CharacterTexts { get; private set; } = new Dictionary<int, CharacterTextData>();

    public Dictionary<int, DialogueData> Stories { get; private set; } = new Dictionary<int, DialogueData>();
/*    public Dictionary<int, BranchData> Branches { get; private set; } = new Dictionary<int, BranchData>();
    public Dictionary<int, EffectData> Effects { get; private set; } = new Dictionary<int, EffectData>();
    public Dictionary<string, MapData> Maps { get; private set; } = new Dictionary<string, MapData>();*/

    private string JsonPath = "2Data/Json";

    public IEnumerator Init()
    {

        TextAsset _textAsset = Managers.Resource.Load<TextAsset>($"{JsonPath}/StartData");
        Start = JsonUtility.FromJson<StartData>(_textAsset.text);

        yield return Managers.StartCoroutineStatic(LoadData("UITextData", (json) =>
        {
            UITexts = JsonUtility.FromJson<UITextDataLoader>(json).MakeDict();

        }));

        if (Managers.Instance.���丮_��ŵ == true)
            yield break;

        yield return Managers.StartCoroutineStatic(LoadData("CharacterTextData", (json) =>
        {
            CharacterTexts = JsonUtility.FromJson<CharacterTextDataLoader>(json).MakeDict();

        }));

        yield return Managers.StartCoroutineStatic(LoadData("CharacterData", (json) =>
        {
            Characters = JsonUtility.FromJson<_CharacterDataLoader>(json).MakeDict();

            foreach(var character in Characters)
            {
                character.Value.name = CharacterTexts[character.Value.textID + Define.nameID];
                character.Value.position = CharacterTexts[character.Value.textID + Define.positionID];

            }
        }));

        yield return Managers.StartCoroutineStatic(LoadData("StoryData", (json) =>
        {
            Stories = JsonUtility.FromJson<DialogueDataLoader>(json).MakeDict();

            foreach (var dialogue in Stories)
            {
                if (Characters.TryGetValue(dialogue.Value.characterStr, out var charData))
                    dialogue.Value.speaker = charData;
                else
                    continue;

            }
        }));

        /*yield return Managers.StartCoroutineStatic(LoadData("EffectData", (json) =>
        {
            Effects = JsonUtility.FromJson<EffectDataLoader>(json).MakeDict();
        }));

        yield return Managers.StartCoroutineStatic(LoadData("BranchData", (json) =>
        {
            Branches = JsonUtility.FromJson<BranchDataLoader>(json).MakeDict();

            foreach(var  branch in Branches)
            {
                if (Effects.TryGetValue(branch.Value.trueEffectID, out var effect))
                {
                    branch.Value.trueEffect = effect;

                }
                if (Effects.TryGetValue(branch.Value.falseEffectID, out effect))
                {
                    branch.Value.falseEffect = effect;

                }

            }
        }));

        yield return Managers.StartCoroutineStatic(LoadData("Mapdata", (json)=> 
        { 
            Maps = JsonUtility.FromJson<MapDataLoader>(json).MakeDict();

            foreach(var map in Maps)
            {
                map.Value.didVisit = false;
                if(map.Value.npc != "")
                    map.Value.Character = Characters[map.Value.npc];
            }
        
        }));*/

        yield break;
    }

    /*IEnumerator SaveSheetToJson(string sheetName)
    {
        string url = googleSheetsBaseUrl + "?sheet=" + sheetName;
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[DataManager] {sheetName} �ε� ����: {request.error}");
            yield break;
        }

        string jsonData = request.downloadHandler.text;

        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.LogError($"[DataManager] {sheetName} �ε� ����: ������ ����");
            yield break;
        }

        string path = Path.Combine(Application.dataPath, $"Resources/{JsonPath}/{sheetName}.json"); // ���� ���
        File.WriteAllText(path, jsonData); // ���� ���� ����
        Debug.Log($"[DataManager] {sheetName} ���� �Ϸ�: {path}");

    }*/

    IEnumerator LoadData(string sheetName, Action<string> callback)
    {
        string data = Managers.Resource.Load<TextAsset>($"{JsonPath}/{sheetName}").text;
        Debug.Log($"[DataManager] {sheetName} ������ �ε� ����: {data}"); // ��Ʈ �̸� �α� ���
        callback?.Invoke(data);
        yield return null;

    }
}
