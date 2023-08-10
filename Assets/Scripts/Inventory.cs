using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;

    const int InventorySize = 24;

    private void Start()
    {
        RefreshContent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }

    public void RemoveItem(ItemData item)
    {
        content.Remove(item);
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }

    private void RefreshContent()
    {
        for (int slotIndex = 0; slotIndex < content.Count; slotIndex++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(slotIndex).GetComponent<Slot>();
            currentSlot.item = content[slotIndex];
            currentSlot.itemVisual.sprite = content[slotIndex].visual;
        }
    }

    public bool IsFull()
    {
        return content.Count >= InventorySize;
    }
}
