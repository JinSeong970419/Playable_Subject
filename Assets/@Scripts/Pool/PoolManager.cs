using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Ç® ¼¼ÆÃ")]
    public List<PoolItem> poolItem;

    private Dictionary<PoolKey, Queue<GameObject>> _poolDict = new();
    private Dictionary<PoolKey, GameObject> _prefabDict = new();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var item in poolItem)
        {
            Queue<GameObject> objectQueue = new();
            for (int i = 0; i < item.initialSize; i++)
            {
                var obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            _poolDict[item.key] = objectQueue;
            _prefabDict[item.key] = item.prefab;
        }
    }

    public GameObject Spawn(PoolKey key, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject obj = null;

        if (!_poolDict.ContainsKey(key))
            return null;

        if (_poolDict[key].Count > 0)
            obj = _poolDict[key].Dequeue();
        else
            obj = Instantiate(_prefabDict[key]);

        obj.transform.SetPositionAndRotation(position, rotation);

        if(parent != null)
            obj.transform.SetParent(parent);

        obj.SetActive(true);

        return obj;
    }

    public void Despawn(PoolKey key, GameObject obj)
    {
        if(!_poolDict.ContainsKey(key))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(null);
        _poolDict[key].Enqueue(obj);
    }
}
