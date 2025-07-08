using DG.Tweening;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float moveDuration = 3f;
    public float _targetY = 5f;
    public float spawnInterval = 0.3f;

    private Tween _spawnLoop;

    protected virtual void Start()
    {
        //DOVirtual.DelayedCall(spawnInterval, SpawnLine);
    }

    public void BeginSpawning()
    {
        _spawnLoop = DOVirtual.DelayedCall(spawnInterval, () =>
        {
            SpawnLine();
            //BeginSpawning();
        });
    }

    public void StopSpawning()
    {
        if (_spawnLoop != null && _spawnLoop.IsActive())
            _spawnLoop.Kill();
    }

    protected abstract void SpawnLine();

    protected abstract Vector3 GetSpawnPosition(int index);

    protected abstract Vector3 GetTargetPosition(Vector3 spawnPos);
}
