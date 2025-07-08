using DG.Tweening;
using UnityEngine;

public class TraySpawner : SpawnerBase
{
    public float trayEndY = -2f;

    [Header("트레이 세팅")]
    public Transform spawnPoint;

    protected override void SpawnLine()
    {
        PoolKey key = PoolKey.Tray;
        Vector3 spawnPos = spawnPoint.position;

        GameObject obj = PoolManager.Instance.Spawn(key, spawnPos, Quaternion.identity, spawnPoint);
        TrayObject tray = obj.GetComponent<TrayObject>();

        if (tray != null)
        {
            tray.MoveToY(trayEndY, moveDuration, () =>
            {
                PoolManager.Instance.Despawn(key, obj);
            });
        }

        DOVirtual.DelayedCall(spawnInterval, SpawnLine);
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