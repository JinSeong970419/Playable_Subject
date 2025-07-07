using UnityEngine;

public enum PoolKey
{
    BottledDrink,
    BottleJuice,
    Danji,
    Gimbab,
    LongPringles,
    Milk,
    Pepero,
    TwinBars,
    Yoplait,

    // ∆Æ∑π¿Ã
    Tray
}

[System.Serializable]
public class PoolItem : MonoBehaviour
{
    public PoolKey key;
    public GameObject prefab;
    public int initialSize = 10;
}