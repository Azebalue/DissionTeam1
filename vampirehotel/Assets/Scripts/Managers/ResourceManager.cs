using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T> (string path) where T : Object
    {
/*        if (Resources.Load<T>(path) == null)
            Debug.Log($"[Load] �����, ���� �ּ�:{path} ");*/

        return Resources.Load<T>(path);
    }


    public Sprite LoadSprite (string path)
    {
/*        Sprite sprite = Resources.Load<Sprite>($"3Sprites/{path}");

        if (sprite == null)
            Debug.Log($"3Sprites/{path}�� ���̴�");

        return sprite;
*/

        return Resources.Load<Sprite>($"3Sprites/{path}");
    }


    public ScriptableObject LoadAsset(string path)
    {
        return Resources.Load<ScriptableObject>($"2Data/{path}");
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"1Prefabs/{path}");
        if(prefab == null)
        {
            Debug.Log($"[���ҽ� �Ŵ���]���� to load prefab: {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go) {

        if (go == null)
            return;

        Object.Destroy(go);
    }

}
