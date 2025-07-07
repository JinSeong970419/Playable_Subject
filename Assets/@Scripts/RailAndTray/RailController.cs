using UnityEngine;
using DG.Tweening;

public class RailSpawner : SpawnerBase
{
    public float railTargetOffset = 8f;

    protected override void SpawnLine()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 spawnPos = spawnPoints[i].position;

            PoolKey[] keys = (PoolKey[])System.Enum.GetValues(typeof(PoolKey));
            int rand = Random.Range(0, keys.Length -1);
            PoolKey key = keys[rand];

            GameObject obj = PoolManager.Instance.Spawn(key, spawnPos, Quaternion.identity, spawnPoints[i]);
            RailObject railObj = obj.GetComponent<RailObject>();

            if (railObj != null)
            {
                railObj.MoveToY(_targetY, moveDuration, () =>
                {
                    PoolManager.Instance.Despawn(key, obj);
                });
            }
        }

        DOVirtual.DelayedCall(spawnInterval, SpawnLine);
    }

    protected override Vector3 GetSpawnPosition(int index)
    {
        return spawnPoints[index].position;
    }

    protected override Vector3 GetTargetPosition(Vector3 spawnPos)
    {
        return new Vector3(spawnPos.x, spawnPos.y + railTargetOffset, spawnPos.z);
    }
}