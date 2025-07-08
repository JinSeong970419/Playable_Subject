using DG.Tweening;
using UnityEngine;

public class TraySlot : MonoBehaviour
{
    [SerializeField] private PoolKey slotKey;
    [SerializeField] private Transform receivePoint;

    private RailObject _current;

    public bool CanAccept(PoolKey incomingKey) => _current == null && incomingKey == slotKey;

    public void Receive(RailObject railObj)
    {
        _current = railObj;
        railObj.DOKill();

        railObj.transform.SetParent(transform);
        railObj.transform.DOMove(transform.position, 0.4f).SetEase(Ease.InOutSine).OnComplete(() => 
        {
            railObj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        });
    }

    public void Clear()
    {
        if (_current == null)
            return;

        PoolManager.Instance.Despawn(_current.PoolKey, _current.gameObject);
        _current = null;
    }

    public bool IsOccupied => _current != null;
}