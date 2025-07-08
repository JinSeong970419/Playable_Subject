using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TraySpawner : SpawnerBase
{
    public static TraySpawner Instance { get; private set; }

    [SerializeField] private List<TrayObject> trayObjects = new();

    [LunaPlaygroundField("Tray Spawn Interval (x10)", 50, "Tray spawn interval in 0.1 sec units")]
    public int traySpawnIntervalRaw = 50;

    protected override float SpawnInterval => traySpawnIntervalRaw * 0.1f;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float trayEndY = -2f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    protected override void SpawnLine()
    {
        var key = PoolKey.Tray;
        var spawnPos = spawnPoint.position;

        var obj = PoolManager.Instance.Spawn(key, spawnPos, Quaternion.identity, spawnPoint);
        var tray = obj.GetComponent<TrayObject>();

        var keys = (PoolKey[])System.Enum.GetValues(typeof(PoolKey));
        var randomKey = keys[Random.Range(0, keys.Length - 1)];
        tray.SetTargetKey(randomKey);

        trayObjects.Add(tray);

        tray.MoveToY(trayEndY, MoveDuration, () =>
        {
            trayObjects.Remove(tray);
            tray.Clear();
            PoolManager.Instance.Despawn(key, obj);
        });
    }

    public void NotifyTrayObjects(RailObject clickedRail)
    {
        foreach (var tray in trayObjects) tray.OnRailClicked(clickedRail);
    }
}