using DG.Tweening;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float moveDuration = 3f;
    public float _targetY = 5f;
    public float spawnInterval = 0.3f;

    protected virtual void Start()
    {
        DOVirtual.DelayedCall(spawnInterval, SpawnLine);
    }

    protected abstract void SpawnLine();

    protected abstract Vector3 GetSpawnPosition(int index);

    protected abstract Vector3 GetTargetPosition(Vector3 spawnPos);
}
