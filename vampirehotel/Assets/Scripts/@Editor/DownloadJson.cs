#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;
using UnityEditor.Toolbars;
using System;

public class CustomWindow : EditorWindow
{
    [MenuItem("Custom/DownloadJson")] //�ش� ��ư�� ������ Init �Լ��� ����
    private static void DownloadWindow()
    {
        string[] names = Enum.GetNames(typeof(data));
        foreach(string dataName in names)
        {
            SaveSheetToJson($"{dataName}");
        }

/*        CustomWindow eidtorWindow = GetWindow<CustomWindow>(); //������ ����
        eidtorWindow.Show(); //������ ����*/
        

    }

    enum data { 
        UITextData,
        CharacterData,
        CharacterTextData,
        StoryData,
        BranchData,
        EffectData,
        MapData,
    }
    bool[] isSelected = new bool[Enum.GetNames(typeof(data)).Length];

    private void OnGUI()
    {
        EditorGUILayout.LabelField("�ٿ�ε��� Google ��Ʈ�� üũ�ϼ���");
        //EditorGUILayout.Toggle(isSelected, bool);
    }

    private static string JsonPath = "2Data/Json";
    private const string googleSheetsBaseUrl =
    "https://script.google.com/macros/s/AKfycbxEiyQLkjWDytu2-BhtvFv920zSm8Iob9xuks4UEcQurm11md5y07GFGfVaqHxNI1CSxQ/exec";

    public static void SaveSheetToJson(string sheetName)
    {
        string url = googleSheetsBaseUrl + "?sheet=" + sheetName;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();

            while(operation.isDone == false)
            {
                //�� �ٿ� �ɶ����� ���� ���
            }

            //��û�� ���������� üũ
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{sheetName} �ε� ����: {request.error}");
                return;
            }


            string jsonData = request.downloadHandler.text;
            //�����͸� ��Ʈ������ �� ��ȯ�ߴ���(�� ���ڿ��� �ƴ���)
            if (string.IsNullOrEmpty(jsonData))
            {
                Debug.LogError($"{sheetName} �ε� ����: ������ ����");
                return;
            }

            string path = Path.Combine(Application.dataPath, $"Resources/{JsonPath}/{sheetName}.json"); // ���� ���
            File.WriteAllText(path, jsonData); // ���� ���� ����
            Debug.Log($"[DataManager] {sheetName} ���� �Ϸ�: {path}");
        }





    }
}
#endif