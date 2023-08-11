using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private ItemData itemCurrentlySelected;

    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;

    [SerializeField]
    private Sprite emptySlotVisual;

    private const int InventorySize = 24;

    [Header("Action Panel References")]
    [SerializeField]
    private GameObject actionPanel;

    [SerializeField]
    private GameObject useItemButton;

    [SerializeField]
    private GameObject equipItemButton;

    [SerializeField]
    private GameObject dropItemButton;

    [SerializeField]
    private GameObject destroyItemButton;

    private void Awake()
    {
        instance = this;
    }

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
        for (int slotIndex = 0; slotIndex < inventorySlotsParent.childCount; slotIndex++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(slotIndex).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
        }

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

    // Action Panel Functions
    public void OpenActionPanel(ItemData item)
    {
        itemCurrentlySelected = item;

        if (item == null)
            return;
        switch (item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
            default:
                break;
        }

        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    // Buttons Functions
    public void UseActionButton()
    {
        print("Use item : " + itemCurrentlySelected.name);
        CloseActionPanel();
    }

    public void EquipActionButton()
    {
        print($"Equip item : {itemCurrentlySelected.name}");
        CloseActionPanel();
    }

    public void DropActionButton()
    {
        CloseActionPanel();
    }

    public void DestroyActionButton()
    {
        RemoveItem(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }
}
