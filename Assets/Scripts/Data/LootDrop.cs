using UnityEngine;

[System.Serializable]
public struct LootDrop
{
    public ItemData item;
    [Range(0f, 1f)] public float dropChance;
}
