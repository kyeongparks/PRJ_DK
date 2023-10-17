using System.Collections.Generic;
using UnityEngine;


public static class UIUtils
{
    public static string GetFullPath(Transform transform, string path = null)
    {
        Transform t = transform;
        string fullPath;
        if (path != null && path.StartsWith("/"))
        {
            fullPath = path;
        }
        else
        {
            fullPath = string.Empty;
            while (t != null)
            {
                if (string.IsNullOrEmpty(fullPath))
                    fullPath = t.name;
                else
                    fullPath = string.Concat(t.name, "/", fullPath);
                t = t.parent;
            }
            if (!string.IsNullOrEmpty(path))
            {
                fullPath += string.Concat("/", path);
            }
        }
        return fullPath;
    }

    static Transform FindTransform(Transform transform, string path)
    {
        Transform t = transform.Find(path);
        if (t == null)
        {
            Debug.LogWarningFormat("Child not found : path={0}", GetFullPath(transform, path));
            return null;
        }

        return t;
    }

    public static GameObject Find(Transform transform, string path)
    {
        Transform t = FindTransform(transform, path);
        return t == null ? null : t.gameObject;
    }

    public static GameObject Find(Transform transform, string format, params object[] args)
    {
        return Find(transform, string.Format(format, args));
    }

    public static T Find<T>(Transform transform, string path) where T : Component
    {
        Transform t = FindTransform(transform, path);
        if (t == null) return null;

        T component = t.GetComponent<T>();
        if (component == null)
        {
            Debug.LogWarningFormat("{0} not found : path={1}", typeof(T).Name, GetFullPath(transform, path));
        }

        return component;
    }

    public static T Find<T>(Transform transform, string format, params object[] args) where T : Component
    {
        return Find<T>(transform, string.Format(format, args));
    }

    public static GameObject Find(this GameObject go, string path)
    {
        return Find(go.transform, path);
    }

    public static GameObject Find(this GameObject go, string format, params object[] args)
    {
        return Find(go.transform, string.Format(format, args));
    }

    public static T Find<T>(this GameObject go, string path) where T : Component
    {
        return Find<T>(go.transform, path);
    }

    public static T Find<T>(this GameObject go, string format, params object[] args) where T : Component
    {
        return Find<T>(go.transform, string.Format(format, args));
    }

    public static GameObject Find(Component component, string path)
    {
        return Find(component.transform, path);
    }

    public static GameObject Find(Component component, string format, params object[] args)
    {
        return Find(component.transform, string.Format(format, args));
    }

    public static T Find<T>(Component component, string path) where T : Component
    {
        return Find<T>(component.transform, path);
    }

    public static T Find<T>(Component component, string format, params object[] args) where T : Component
    {
        return Find<T>(component.transform, string.Format(format, args));
    }

    public static List<GameObject> FindAll(Transform transform, string path)
    {
        List<GameObject> all = new List<GameObject>();
        int index = path.LastIndexOf('/');
        string name = index < 0 ? path : path.Substring(index + 1);

        Transform found = FindTransform(transform, path);
        if (found != null)
        {
            Transform parent = found.parent;
            if (parent != null)
            {
                int childCount = parent.childCount;
                for (int i = 0; i < childCount; ++i)
                {
                    Transform child = parent.GetChild(i);
                    if (child.name == name)
                        all.Add(child.gameObject);
                }
            }
        }

        return all;
    }

    public static List<T> FindAll<T>(Transform transform, string path) where T : Component
    {
        List<T> all = new List<T>();
        foreach (GameObject go in FindAll(transform, path))
        {
            T component = go.GetComponent<T>();
            if (component != null)
                all.Add(component);
        }

        return all;
    }

    public static List<GameObject> FindAll(this GameObject go, string path)
    {
        return FindAll(go.transform, path);
    }

    public static List<T> FindAll<T>(this GameObject go, string path) where T : Component
    {
        return FindAll<T>(go.transform, path);
    }

    public static T FindInParents<T>(this GameObject go) where T : Component
    {
        if (go == null) return null;
        Transform t = go.transform;
        while (t != null)
        {
            T comp = t.gameObject.GetComponent<T>();
            if (comp != null)
                return comp;

            t = t.parent;
        }

        return null;
    }

    public static GameObject Instantiate(GameObject original, Transform parent, string name = null)
    {
        GameObject child = Object.Instantiate(original);
        if (!string.IsNullOrEmpty(name))
            child.name = name;

        Transform t = child.transform;
        t.localRotation = Quaternion.identity;
        SetParent(t, parent);

        return child;
    }

    public static void SetParent(Transform t, Transform parent)
    {
        t.SetParent(parent);
        t.localScale = Vector3.one;
        t.gameObject.layer = parent.gameObject.layer;

        RectTransform rt = t.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition3D = Vector3.zero;
        }
        else
        {
            t.localPosition = Vector3.zero;
        }
    }

    public static GameObject InstantiateEffect(GameObject original, Transform parent, bool resetScale = false)
    {
        GameObject child = Object.Instantiate(original);
        child.transform.parent = parent;
        child.transform.localRotation = Quaternion.identity;
        child.transform.localPosition = Vector3.zero;
        child.transform.gameObject.layer = parent.gameObject.layer;
        if (resetScale)
            child.transform.localScale = Vector3.one;

        return child;
    }
}

