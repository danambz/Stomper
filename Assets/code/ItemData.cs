using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;
    public ItemType type;
    [TextArea] public string description;

    [Header("Consumable Stats")]
    public int healthRestore = 0;
    public float speedMultiplier = 1f;
    public float speedDuration = 0f;

    [Header("Jump Boost Stats")]
    public float jumpMultiplier = 1f;
    public float jumpDuration = 0f;

    [Header("World Pickup Prefab")]
    [Tooltip("The prefab to spawn when dropping this item")]
    public GameObject worldPrefab;
}
