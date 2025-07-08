using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrayObject : MonoBehaviour
{
    [SerializeField] private List<TrayObject> trayObjects;
    [SerializeField] private Transform poolKeyUI;

    private PoolKey poolKey;  // TrayObject Ű
    public PoolKey PoolKey => poolKey;

    [SerializeField] private List<TraySlot> traySlots;
    private PoolKey targetKey;
    private GameObject targetobj;

    public void SetTargetKey(PoolKey key)
    {
        targetKey = key;
        targetobj = PoolManager.Instance.Spawn(key, poolKeyUI.position, Quaternion.identity, poolKeyUI);
        targetobj.transform.localScale = new Vector3(5f, 5f, 5f);
    }

    public void Clear()
    {
        PoolManager.Instance.Despawn(targetKey, targetobj);
        traySlots.ForEach(x => x.Clear());
    }

    public void OnRailClicked(RailObject railobj)
    {
        if (railobj.PoolKey != targetKey) return;

        foreach (var slot in traySlots)
        {
            if (!slot.IsOccupied)
            {
                railobj.MoveStop();
                slot.Receive(railobj);
                return;
            }
        }
    }

    public void MoveToY(float targetY, float duration, Action onComplete)
    {
        transform.DOKill();

        Vector3 target = new(transform.position.x, targetY, transform.position.z);
        transform.DOMove(target, duration).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
    }
}
