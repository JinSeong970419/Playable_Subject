using DG.Tweening;
using System;
using UnityEngine;

public class TraySlot : MonoBehaviour
{
    [SerializeField] private PoolKey slotKey;
    [SerializeField] private Transform receivePoint;

    private RailObject _current;

    public bool IsOccupied => _current != null;
    public bool CanAccept(PoolKey incomingKey) => !IsOccupied && incomingKey == slotKey;

    public void Receive(RailObject railObj, Action onComplete)
    {
        _current = railObj;
        railObj.DOKill();
        railObj.transform.SetParent(transform);

        // 트레이 담기는 모션
        railObj.transform.DOScale(1.2f, 0.1f)
            .OnComplete(() => railObj.transform.DOScale(1f, 0.4f).SetEase(Ease.OutQuad));

        railObj.transform.DOMove(receivePoint.position, 0.4f).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                railObj.transform.SetPositionAndRotation(receivePoint.position, transform.rotation);
                railObj.transform.DOScale(1.1f, 0.05f).OnComplete(() => railObj.transform.DOScale(1f, 0.05f));
                onComplete?.Invoke();
            });
    }

    public void Clear()
    {
        if (!IsOccupied) return;

        PoolManager.Instance.Despawn(_current.PoolKey, _current.gameObject);
        _current = null;
    }
}