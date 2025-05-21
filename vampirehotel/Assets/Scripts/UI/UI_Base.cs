using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    //UI ������Ʈ �������� �����ϴ� ��ųʸ�
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    
    protected bool _init = false;

    public void Start()
    {
        Init();
    }

    public virtual bool Init()
    {
        if (_init)
            return false;

        return _init = true;
    }

    #region Bind�迭 �޼���
    //UI�������� ������Ʈ�� ��ųʸ��� �ִ� �޼���
    protected void Bind<T>(Type type, bool includeInactive = true) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); //��ǻ� type�� enum�̴�

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        _objects.Add(typeof(T), objects); //��ųʸ��� �߰�, �׷��� ���� ������ X


        //Ž���ϸ� ������Ʈ ���� : �ֻ�� obj������ �� ��ȸ�ϸ� ���� �̸� �ִ� ��
        for (int i = 0; i < names.Length; i++)
        {
            //������Ʈ�� �ƴ� ���� ������Ʈ�� ������ ��� ����
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true, includeInactive);
            }

            //������Ʈ ������ ���
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true, includeInactive);
            }

/*            if (objects[i] == null)
                Debug.LogError($"[Bind] ���� to find object: {names[i]}");
            else
                Debug.Log($"[Bind ����]: {names[i]}");*/


        }

    }


    protected void BindNested<T>(Type type, string childName = null, bool includeInactive = true) where T : UnityEngine.Object
    {
        string[] parentNames = Enum.GetNames(type);
        int startIndex;
        Type _type = typeof(T);
        UnityEngine.Object[] objects;

        if (_objects.ContainsKey(_type) == true)
        {
            startIndex = _objects[_type].Length;
            objects = new UnityEngine.Object[startIndex + parentNames.Length];
            Array.Copy(_objects[_type], objects, startIndex);
        }
        else
        {
            objects = new UnityEngine.Object[parentNames.Length];
            startIndex = 0;

        }


        for (int i = 0; i < parentNames.Length; i++)
        {
            GameObject parent = Util.FindChild(gameObject, parentNames[i], true, includeInactive);
            objects[startIndex+i] = Util.FindChild<T>(parent, childName, false, includeInactive);
        }

        _objects[_type] = objects;
    }
    #endregion

    #region Get�迭�Լ�
    protected void GetTexts(int startEnum, int endEnum, int startDefine)
    {
        for (int textID = startEnum; textID <= endEnum; textID++)
            GetText(textID).text = Managers.GetText(startDefine + (textID - startEnum));
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        //��ųʸ��� _objects�� Ÿ���� ������ ������, ������ �� null��ȯ&����
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        try
        {
            //������ UnityEngine.Object Ÿ�Կ��� T�� ĳ�����ؼ� ��ȯ
            return objects[idx] as T;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"{idx}:{e}");
            return objects[0] as T;
        }
    }

    //���� ����ϴ� ��ҿ� ���� ���Լ� ������ 3��
    protected TextMeshProUGUI GetText(int idx)
    {
        return Get<TextMeshProUGUI>(idx);
    }
    
    protected Button GetButton(int idx) { return Get<Button>(idx); }

    protected Image GetImage(int idx) { return Get<Image>(idx); }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx);  }

    protected Sprite GetCharacter(string name)
    {
        try
        {
            return Managers.Resource.LoadSprite($"Characters/{name}");
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning($"{name}�� �̹����� �����ϴ�. ���� �̹����� ��ü��:{e}");
            return Managers.Resource.LoadSprite($"Characters/Prince");

        }
    }

    #endregion


    //Ư�� UI ������Ʈ�� ��ȣ�ۿ� ����� �ٿ��ִ� �޼���
    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        /*����:
        go ���� ������Ʈ�� �ٿ����Ҵ� ��ũ��Ʈ(UI_EventHandler)�� GetComponent(����) => evt
        �� �̺�Ʈ�� �޼ҵ�(action) ������Ű��.
        �̺�Ʈ �� �ڵ鷯 ���� (Define���� ����)
         */
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        if (evt == null)
        {
            Debug.LogError($"[AddUIEvent] {go.name}�� UI_EventHandler�� �߰��� �� ����.");
            return;
        }
        

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Enter:
                evt.OnEnterHandler -= action;
                evt.OnEnterHandler += action;
                break;

            case Define.UIEvent.Exit:
                evt.OnExitHandler -= action;
                evt.OnExitHandler += action;
                break;
        }

    }

    public virtual void Bind()
    {
    }

    public virtual void Gets()
    {
    }
    

}
