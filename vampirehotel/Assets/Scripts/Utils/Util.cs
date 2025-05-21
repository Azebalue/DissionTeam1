using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//��ɼ� �Լ����� �� ����
public class Util 
{
    //������Ʈ ���� �Լ� (�ֻ����θ� go, ã�� obj�̸�, ����� Ž�� ����)
    //*���⼭ ����� Ž���� �θ��� �ڽĸ� ã�� �� �ƴϰ�, �ڽ��� �ڽĵ� ã�� �� �ǹ�=>(Ʈ��� �ϴ� �� ����)
    public static T FindChild<T> (GameObject go, string name= null, bool recursive = false, bool includeInactive = false) where T : UnityEngine.Object
    {
        if (go == null) 
            return null;

        //1. ���Ӹ� Ž���ϴ� ���
        if (recursive == false)
        {
            //Transform������Ʈ�� �ڽ� ������Ʈ�� ����
            for (int i = 0; i < go.transform.childCount; i++)
            {
                UnityEngine.Transform transform = go.transform.GetChild(i);
                //Debug.Log($"[����FindChild] Checking child: {transform.gameObject.name}");

                // �̸��� �� �־����� �׳� TŸ�� ��ȯ & �̸��� �´°� ã���� ���
                if (string.IsNullOrEmpty(name) || transform.gameObject.name == name)
                {
                    T component = transform.GetComponent<T>();

                    if (component != null)
                    {
                        //Debug.Log($"[FindChild] Found matching component in: {transform.gameObject.name}");
                        return component;
                    }
                }
            }
        }
        else //2. ��������� �ڼ� Ž��
        {
            foreach (T component in go.GetComponentsInChildren<T>(includeInactive))
            {
                //Debug.Log($"[���FindChild] Checking child: {component.name}");
                // �̸��� �� �־����� �׳� TŸ�� ��ȯ & �̸��� �´°� ã���� ���
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;



    }

    //1-2. ������Ʈ ���� �Լ�
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false, bool isActivated = false)
    {
        UnityEngine.Transform transform = FindChild<UnityEngine.Transform>(go, name, recursive, isActivated);

        if (transform == null)
            return null;

        return transform.gameObject;

    }

    //2. ��η� ã�� �Լ�
    public static T FindChildByPath<T>(GameObject root, string path, bool includeInactive = false) where T : UnityEngine.Object
    {
        if (root == null || string.IsNullOrEmpty(path))
            return null;

        Transform current = root.transform;
        string[] segments = path.Split('/');

        current = FindChild(root, segments[0], true).transform;
        for(int i = 1; i < segments.Length; i++)
        {
            Transform child = current.Find(segments[i]);
            if (child == null)
            {
                Debug.LogWarning($"[FindChildByPath] '{segments[i]}'��(��) '{current.name}'���� ã�� �� �����ϴ�.");
                return null;
            }
            current = child;
        }

        T component = current.GetComponent<T>();

        if(current.GetComponent<TextMeshPro>() == null)
        {
            Debug.Log($"'{current.name}'���� TextMeshPro������Ʈ�� ã�� �� �����ϴ�.");

        }
        if (component == null)
            Debug.LogWarning($"[FindChildByPath] '{current.name}'���� {typeof(T).Name} ������Ʈ�� ã�� �� �����ϴ�. \n T:{typeof(T)}");

        return component;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static Canvas FindFirstCanvasInHierarchy()
    {
        // �� ���� ��� Canvas ��������
        Canvas[] allCanvases = GameObject.FindObjectsOfType<Canvas>();

        if (allCanvases.Length > 0)
        {
            //Debug.Log($"[CanvasFinder] ù ��° Canvas({allCanvases[0].gameObject.name})�� ã��.");
            return allCanvases[0]; // ù ��° Canvas ��ȯ
        }

        Debug.LogError("[CanvasFinder] ������ Canvas�� ã�� �� ����!");
        return null;
    }

    public static int GetEnumCount<T>() where T : Enum
    {
        return Enum.GetNames(typeof(T)).Length;
    }
    public static void ApplySliderEffect(Slider slider, int startValue, int deltaValue)
    {

    }
}
