using System;
using System.Collections;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region 0. Instance
    static Managers _instance; //유일성 보장
    public static Managers Instance { get { Init(); return _instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    SceneManagerEx _scene = new SceneManagerEx();
    DataManager _data = new DataManager();
    GameManagerEx _game = new GameManagerEx();
    TimeManager _time = new TimeManager();
    StoryManager _story = new StoryManager();
    StatManager _stat = new StatManager();


    public static ResourceManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static DataManager Data { get { return Instance._data; } }
    public static GameManagerEx Game { get { return Instance._game; } }
    public static TimeManager Time { get {  return Instance._time; } }
    public static StoryManager Story { get { return Instance._story; } }
    public static StatManager Stat { get { return Instance._stat; } }

    #endregion

    #region 디버깅 모드 관리
    [Header("Data")]
    public bool Write = false;



    [SerializeField] public float coeffient = 0.5f;

    #endregion

    static Action init;
    public static string savePath;
    public static bool IsPreloaded { get; private set; } = false;

    public static string GetText(int id)
    {
        // 1?. 주어진 ID에 해당하는 텍스트 데이터를 가져옴
        if (Managers.Data.UITexts.TryGetValue(id, out UITextData value) == false)
        {
            Debug.Log($"{id}의 텍스트 값 없어서 못넣음");
            return ""; // ID가 존재하지 않으면 빈 문자열 반환

        }

        return value.kor;
    }

    public static IEnumerator StartCoroutineStatic(IEnumerator coroutine)
    {
        if (Instance != null)
            yield return _instance.StartCoroutine(coroutine);
    }


    void Start()
    {
        savePath = Application.persistentDataPath + "/SaveData.json";
        Init();
    }


    void Update()
    {
        _input.OnUpdate();
        Time.SimulateTime();
    }

    static void Init()
    {

        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            GameObject scene = GameObject.Find("@Scene");
            if (scene == null)
            {
                scene = new GameObject { name = "@Scene" };
                DontDestroyOnLoad(scene); //삭제 방지
            }

            DontDestroyOnLoad(go); //삭제 방지
            _instance = go.GetComponent<Managers>();

            init += () => { GameObject.Find("@Scene").AddComponent<GameScene>(); };
        }

        //Time.InitTime();
    }

    private IEnumerator PreloadData()
    {
        if (IsPreloaded == true) 
            yield break;

        yield return _data.Init(); // 데이터 매니저 초기화 (비동기 로딩)
        IsPreloaded = true; // 프리로딩 완료 플래그 설정
        UI.CloseSceneUI();

        init.Invoke();
    }

}

