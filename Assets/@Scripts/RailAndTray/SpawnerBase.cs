using DG.Tweening;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    [SerializeField] protected Transform[] spawnPoints;
    [SerializeField] private float moveDuration = 3f;
    [SerializeField] private float targetY = 5f;

    private Sequence _spawnLoop;

    protected float MoveDuration => moveDuration;
    protected float TargetY => targetY;
    protected abstract float SpawnInterval { get; }

    protected virtual void OnEnable() => BeginSpawning();
    protected virtual void OnDisable() => StopSpawning();

    public void BeginSpawning()
    {
        StopSpawning();
        _spawnLoop = DOTween.Sequence()
            .AppendCallback(SpawnLine)
            .AppendInterval(SpawnInterval)
            .SetLoops(-1);
    }

    public void StopSpawning() => _spawnLoop?.Kill();

    protected abstract void SpawnLine();
}
