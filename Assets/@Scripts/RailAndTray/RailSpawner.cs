using UnityEngine;

public class RailSpawner : SpawnerBase
{
    [LunaPlaygroundField("Object Spawn Interval (x10)", 10, "Object spawn interval in 0.1 sec units")]
    public int objectSpawnIntervalRaw = 10;

    protected override float SpawnInterval => objectSpawnIntervalRaw * 0.1f;

    [SerializeField] private float railTargetOffset = 8f;
    private Vector3 targetScale = new Vector3(30, 30, 10);

    protected override void SpawnLine()
    {
        var keys = (PoolKey[])System.Enum.GetValues(typeof(PoolKey));

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var spawnPos = spawnPoints[i].position;
            var key = keys[Random.Range(0, keys.Length - 1)];

            var obj = PoolManager.Instance.Spawn(key, spawnPos, Quaternion.identity, spawnPoints[i]);
            var railObj = obj.GetComponent<RailObject>();
            railObj.transform.localScale = targetScale;

            if (railObj == null) continue;

            railObj.Init(key);
            railObj.MoveToY(spawnPos.y + railTargetOffset, MoveDuration, () =>
            {
                PoolManager.Instance.Despawn(key, obj);
            });
        }
    }
}