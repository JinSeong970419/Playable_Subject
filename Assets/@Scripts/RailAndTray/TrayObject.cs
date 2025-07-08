using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrayObject : MonoBehaviour
{
    [SerializeField] private Transform poolKeyUI;
    [SerializeField] private TextMeshProUGUI objCount;
    [SerializeField] private List<TraySlot> traySlots;

    private PoolKey targetKey;
    private GameObject targetObj;
    private int trayCount;

    public void SetTargetKey(PoolKey key)
    {
        targetKey = key;
        targetObj = PoolManager.Instance.Spawn(key, poolKeyUI.position, Quaternion.identity, poolKeyUI);
        targetObj.transform.localScale = new Vector3(5, 5, 1);
    }

    public void Clear()
    {
        if (targetObj != null)
        {
            targetObj.transform.localScale = new Vector3(100,100,1);
            PoolManager.Instance.Despawn(targetKey, targetObj);
            targetObj = null;
        }

        foreach (var slot in traySlots) slot.Clear();

        trayCount = 0;
        UpdateCountUI();
    }

    public void OnRailClicked(RailObject railObj)
    {
        if (railObj.PoolKey != targetKey) return;

        foreach (var slot in traySlots)
        {
            if (!slot.IsOccupied)
            {
                railObj.MoveStop();
                slot.Receive(railObj, () =>
                {
                    trayCount++;
                    UpdateCountUI();
                });
                break;
            }
        }
    }

    public void MoveToY(float targetY, float duration, Action onComplete)
    {
        transform.DOKill();
        var target = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.DOMove(target, duration).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
    }

    private void UpdateCountUI() => objCount.text = string.Concat("/ ", traySlots.Count - trayCount);
}