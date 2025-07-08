using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TraySpawner : SpawnerBase
{
    public static TraySpawner Instance { get; private set; }

    [SerializeField] private List<TrayObject> trayObjects;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public float trayEndY = -2f;

    [Header("트레이 세팅")]
    public Transform spawnPoint;

    protected override void SpawnLine()
    {
        PoolKey key = PoolKey.Tray;
        Vector3 spawnPos = spawnPoint.position;

        GameObject obj = PoolManager.Instance.Spawn(key, spawnPos, Quaternion.identity, spawnPoint);
        TrayObject tray = obj.GetComponent<TrayObject>();

        var randomKey = (PoolKey)Random.Range(0, System.Enum.GetValues(typeof(PoolKey)).Length -1);
        tray.SetTargetKey(randomKey);

        if (tray != null)
        {
            trayObjects.Add(tray);
            tray.MoveToY(trayEndY, moveDuration, () =>
            {
                trayObjects.Remove(tray);
                tray.Clear();
                PoolManager.Instance.Despawn(key, obj);
            });
        }

        DOVirtual.DelayedCall(spawnInterval, SpawnLine);
    }

    public void NotifyTrayObjects(RailObject clickedRail)
    {
        foreach (var tray in trayObjects)
        {
            tray.OnRailClicked(clickedRail);
        }
    }

    protected override Vector3 GetSpawnPosition(int index)
    {
        return spawnPoints[index].position;
    }

    protected override Vector3 GetTargetPosition(Vector3 spawnPos)
    {
        return new Vector3(spawnPos.x, trayEndY, spawnPos.z);
    }
}