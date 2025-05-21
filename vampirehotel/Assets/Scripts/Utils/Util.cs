using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//기능성 함수들을 다 넣음
public class Util 
{
    //컴포넌트 맵핑 함수 (최상위부모 go, 찾는 obj이름, 재귀적 탐색 여부)
    //*여기서 재귀적 탐색은 부모의 자식만 찾는 게 아니고, 자식의 자식도 찾는 걸 의미=>(트루로 하는 게 좋지)
    public static T FindChild<T> (GameObject go, string name= null, bool recursive = false, bool includeInactive = false) where T : UnityEngine.Object
    {
        if (go == null) 
            return null;

        //1. 직속만 탐색하는 경우
        if (recursive == false)
        {
            //Transform컴포넌트는 자식 오브젝트도 포함
            for (int i = 0; i < go.transform.childCount; i++)
            {
                UnityEngine.Transform transform = go.transform.GetChild(i);
                //Debug.Log($"[직속FindChild] Checking child: {transform.gameObject.name}");

                // 이름을 안 넣었으면 그냥 T타입 반환 & 이름이 맞는걸 찾으면 뱉기
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
        else //2. 재귀적으로 자손 탐색
        {
            foreach (T component in go.GetComponentsInChildren<T>(includeInactive))
            {
                //Debug.Log($"[재귀FindChild] Checking child: {component.name}");
                // 이름을 안 넣었으면 그냥 T타입 반환 & 이름이 맞는걸 찾으면 뱉기
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;



    }

    //1-2. 오브젝트 맵핑 함수
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false, bool isActivated = false)
    {
        UnityEngine.Transform transform = FindChild<UnityEngine.Transform>(go, name, recursive, isActivated);

        if (transform == null)
            return null;

        return transform.gameObject;

    }

    //2. 경로로 찾는 함수
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
                Debug.LogWarning($"[FindChildByPath] '{segments[i]}'을(를) '{current.name}'에서 찾을 수 없습니다.");
                return null;
            }
            current = child;
        }

        T component = current.GetComponent<T>();

        if(current.GetComponent<TextMeshPro>() == null)
        {
            Debug.Log($"'{current.name}'에서 TextMeshPro컴포넌트를 찾을 수 없습니다.");

        }
        if (component == null)
            Debug.LogWarning($"[FindChildByPath] '{current.name}'에서 {typeof(T).Name} 컴포넌트를 찾을 수 없습니다. \n T:{typeof(T)}");

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
        // 씬 내의 모든 Canvas 가져오기
        Canvas[] allCanvases = GameObject.FindObjectsOfType<Canvas>();

        if (allCanvases.Length > 0)
        {
            //Debug.Log($"[CanvasFinder] 첫 번째 Canvas({allCanvases[0].gameObject.name})를 찾음.");
            return allCanvases[0]; // 첫 번째 Canvas 반환
        }

        Debug.LogError("[CanvasFinder] 씬에서 Canvas를 찾을 수 없음!");
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
