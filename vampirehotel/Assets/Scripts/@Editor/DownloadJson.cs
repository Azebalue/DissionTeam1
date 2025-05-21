#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;
using UnityEditor.Toolbars;
using System;

public class CustomWindow : EditorWindow
{
    [MenuItem("Custom/DownloadJson")] //해당 버튼을 누르면 Init 함수가 실행
    private static void DownloadWindow()
    {
        string[] names = Enum.GetNames(typeof(data));
        foreach(string dataName in names)
        {
            SaveSheetToJson($"{dataName}");
        }

/*        CustomWindow eidtorWindow = GetWindow<CustomWindow>(); //윈도우 생성
        eidtorWindow.Show(); //윈도우 열기*/
        

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
        EditorGUILayout.LabelField("다운로드할 Google 시트를 체크하세요");
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
                //다 다운 될때까지 수동 대기
            }

            //요청이 성공적인지 체크
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{sheetName} 로드 실패: {request.error}");
                return;
            }


            string jsonData = request.downloadHandler.text;
            //데이터를 스트링으로 잘 변환했는지(빈 문자열이 아닌지)
            if (string.IsNullOrEmpty(jsonData))
            {
                Debug.LogError($"{sheetName} 로드 실패: 데이터 없음");
                return;
            }

            string path = Path.Combine(Application.dataPath, $"Resources/{JsonPath}/{sheetName}.json"); // 저장 경로
            File.WriteAllText(path, jsonData); // 로컬 파일 저장
            Debug.Log($"[DataManager] {sheetName} 저장 완료: {path}");
        }





    }
}
#endif