using DG.Tweening;
using System;
using UnityEngine;

public class TrayObject : MonoBehaviour
{
    public void MoveToY(float targetY, float duration, Action onComplete)
    {
        transform.DOKill();

        Vector3 target = new Vector3(transform.position.x, targetY, transform.position.z);

        transform.DOMove(target, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            if(onComplete != null)
                onComplete();
        });
    }
}
