using System;
using System.Collections;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region 0. Instance
    static Managers _instance; //���ϼ� ����
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

    #region ����� ��� ����
    [Header("Data")]
    public bool Write = false;



    [SerializeField] public float coeffient = 0.5f;

    #endregion

    static Action init;
    public static string savePath;
    public static bool IsPreloaded { get; private set; } = false;

    public static string GetText(int id)
    {
        // 1?. �־��� ID�� �ش��ϴ� �ؽ�Ʈ �����͸� ������
        if (Managers.Data.UITexts.TryGetValue(id, out UITextData value) == false)
        {
            Debug.Log($"{id}�� �ؽ�Ʈ �� ��� ������");
            return ""; // ID�� �������� ������ �� ���ڿ� ��ȯ

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
                DontDestroyOnLoad(scene); //���� ����
            }

            DontDestroyOnLoad(go); //���� ����
            _instance = go.GetComponent<Managers>();

            init += () => { GameObject.Find("@Scene").AddComponent<GameScene>(); };

            //�����ε�
/*            Managers.UI.ShowSceneUI<UI_LoadingScene>();
            if (IsPreloaded == false)
            {
                _instance.StartCoroutine(_instance.PreloadData());
            }*/

            
        }
    
       
    }

    private IEnumerator PreloadData()
    {
        if (IsPreloaded == true) 
            yield break;

        yield return _data.Init(); // ������ �Ŵ��� �ʱ�ȭ (�񵿱� �ε�)
        IsPreloaded = true; // �����ε� �Ϸ� �÷��� ����
        UI.CloseSceneUI();

        init.Invoke();
    }

}

