using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ressource,
    Equipment,
    Consumable,
}

[CreateAssetMenu(fileName = "Item", menuName = "Items/New Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public Sprite visual;
    public GameObject prefab;
}
