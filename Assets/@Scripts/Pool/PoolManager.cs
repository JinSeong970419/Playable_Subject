using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Ç® ¼¼ÆÃ")]
    [SerializeField] private List<PoolItem> poolItems;

    private readonly Dictionary<PoolKey, Queue<GameObject>> _poolDict = new();
    private readonly Dictionary<PoolKey, GameObject> _prefabDict = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var item in poolItems)
        {
            var objectQueue = new Queue<GameObject>();

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
        if (!_poolDict.TryGetValue(key, out var queue) || !_prefabDict.TryGetValue(key, out var prefab))
            return null;

        var obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.transform.SetParent(parent, false);
        obj.SetActive(true);

        return obj;
    }

    public void Despawn(PoolKey key, GameObject obj)
    {
        if (!_poolDict.TryGetValue(key, out var queue))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(null);
        queue.Enqueue(obj);
    }
}
