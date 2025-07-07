using System;
using UnityEngine;
using DG.Tweening;

public class RailObject : MonoBehaviour
{
    private Tween _moveTween;
    private float _moveDuration = 3f;
    private float _targetY = 6f;
    private Action<RailObject> _onReachTop;

    public void Init(float moveDuration, float tragetY, Action<RailObject> onComplete)
    {
        _moveDuration = moveDuration;
        _targetY = tragetY;
        _onReachTop = onComplete;
    }

    public void Activate()
    {
        transform.DOKill();

        var startPos = transform.position;
        var endPos = new Vector3(startPos.x, _targetY, startPos.z);

        _moveTween = transform.DOMove(endPos, _moveDuration).SetEase(Ease.Linear).OnComplete(OnReachTop);
    }

    private void OnReachTop()
    {
        if(_onReachTop != null)
            _onReachTop(this);

        Deactivate();
    }

    private void Deactivate()
    {
        transform.DOKill();
        gameObject.SetActive(false);
    }

    public void MoveToY(float targetY, float duration, System.Action onComplete)
    {
        transform.DOKill();

        Vector3 target = new Vector3(transform.position.x, targetY, transform.position.z);

        transform.DOMove(target, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (onComplete != null)
                onComplete();
        });
    }
}
