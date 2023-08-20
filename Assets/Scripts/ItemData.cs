using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ressource,
    Equipment,
    Consumable,
}

public enum EquipmentType
{
    Head,
    Chest,
    Hands,
    Legs,
    Feets
}

[CreateAssetMenu(fileName = "Item", menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public EquipmentType equipmentType;
    public Sprite visual;
    public GameObject prefab;
}
