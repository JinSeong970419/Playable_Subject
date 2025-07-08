using DG.Tweening;
using UnityEngine;

public class RailObject : MonoBehaviour
{
    public PoolKey PoolKey { get; private set; }

    public void Init(PoolKey key) => PoolKey = key;

    public void MoveToY(float targetY, float duration, System.Action onComplete = null)
    {
        transform.DOKill();
        var target = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.DOMove(target, duration).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
    }

    public void MoveStop() => transform.DOKill();
}