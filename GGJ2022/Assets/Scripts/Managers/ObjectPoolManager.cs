using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPoolManager>();
            }
            return _instance;
        }

    }
    private static ObjectPoolManager _instance;

    private List<int> _poolKeys;
    private Dictionary<int, List<GameObject>> _pool;

    public void InitManager()
    {
        _poolKeys = new List<int>();
        _pool = new Dictionary<int, List<GameObject>>();
    }

    public void DestroyPoolAll()
    {
        // ***** Will cause lag *****
        foreach (var pair in _pool)
        {
            if (pair.Value != null)
            {
                foreach (var obj in pair.Value)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
            }
        }
        _poolKeys = new List<int>();
        _pool = new Dictionary<int, List<GameObject>>();
    }

    public void UncacheObject(GameObject obj)
    {
        // ***** Will cause lag *****

        int key = obj.GetInstanceID();

        if (!_poolKeys.Contains(key))
        {
            return;
        }

        var pooledList = _pool[key];
        //pooledList.RemoveAll(g => g == null);

        foreach (var poolObj in pooledList)
        {
            if (!poolObj.activeSelf)
            {
                Destroy(poolObj);
            }
        }

        _poolKeys.Remove(key);
        _pool.Remove(key);
    }

    public void CacheObject(GameObject obj, int count, Action<GameObject> onObjCreated = null)
    {
        // ***** Will cause lag *****

        int key = obj.GetInstanceID();

        if (_poolKeys.Contains(key) == false && _pool.ContainsKey(key) == false)
        {
            _poolKeys.Add(key);
            _pool.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = _pool[key];
        //goList.RemoveAll(g => g == null);

        int createCount = count - goList.Count;

        for (int i = 0; i < createCount; i++)
        {
            GameObject go = (GameObject)Instantiate(obj, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
            if (onObjCreated != null)
            {
                onObjCreated(go);
            }
            go.SetActive(false);
            goList.Add(go);
        }
    }

    public GameObject CreateObject(GameObject obj, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion) , bool forceCreateNew = false)
    {
        int key = obj.GetInstanceID();

        if (_poolKeys.Contains(key) == false && _pool.ContainsKey(key) == false)
        {
            _poolKeys.Add(key);
            _pool.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = _pool[key];
        //goList.RemoveAll(g => g == null);
        GameObject go = null;

        if (!forceCreateNew)
        {
            for (int i = goList.Count - 1; i >= 0; i--)
            {
                go = goList[i];
                if (go.activeSelf == false)
                {
                    go.transform.position = position;
                    go.transform.rotation = rotation;
                    go.SetActive(true);
                    return go;
                }
            }
        }

        // Instantiate because there is no free GameObject in object pool.
        go = (GameObject)Instantiate(obj, position, rotation);
        go.transform.parent = gameObject.transform;
        goList.Add(go);

        return go;
    }

    public GameObject CreateObject(GameObject obj, Transform _transform, bool forceCreateNew = false)
    {
        int key = obj.GetInstanceID();

        if (_poolKeys.Contains(key) == false && _pool.ContainsKey(key) == false)
        {
            _poolKeys.Add(key);
            _pool.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = _pool[key];
        //goList.RemoveAll(g => g == null);
        GameObject go = null;

        if (!forceCreateNew)
        {
            for (int i = goList.Count - 1; i >= 0; i--)
            {
                go = goList[i];
                if (go.activeSelf == false)
                {
                    go.transform.SetParent(_transform);
                    go.SetActive(true);
                    return go;
                }
            }
        }

        // Instantiate because there is no free GameObject in object pool.
        go = (GameObject)Instantiate(obj, _transform);
        go.transform.SetParent(gameObject.transform);
        goList.Add(go);

        return go;
    }

    public void DestroyObject(GameObject obj, bool forceDestroy = false, bool setTranParentToSelf = true)
    {
        if (forceDestroy)
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        if (setTranParentToSelf)
        {
            obj.transform.SetParent(transform, false);
        }
    }
}
