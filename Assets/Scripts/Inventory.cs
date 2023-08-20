using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private ItemData itemCurrentlySelected;
    private const int InventorySize = 24;

    [Header("Inventory Attributes")]
    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    [SerializeField]
    private Transform inventorySlotsParent;

    [SerializeField]
    private Sprite emptySlotVisual;

    [SerializeField]
    private EquipmentLibrary equipmentLibrary;

    [Header("Utility References")]
    [SerializeField]
    private Transform dropPoint;

    [Header("Canvas References")]
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject actionPanel;

    [SerializeField]
    private GameObject toolTip;

    [Header("Action Panel References")]
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
            if (inventoryPanel.activeSelf)
                CloseInventoryPanel();
            else
                OpenInventoryPanel();
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

    public void OpenInventoryPanel()
    {
        inventoryPanel.SetActive(true);
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        actionPanel.SetActive(false);
        toolTip.SetActive(false);

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
    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        itemCurrentlySelected = item;

        if (item == null)
        {
            CloseActionPanel();
            return;
        }
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

        actionPanel.transform.position = slotPosition;
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

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemCurrentlySelected).First();
        if ( equipmentLibraryItem != null )
        {
            for (int i = 0; i < equipmentLibraryItem.elementsToDisable.Length; i++)
            {
                equipmentLibraryItem.elementsToDisable[i].SetActive(false);
            }
            equipmentLibraryItem.itemPrefab.SetActive(true);
            RemoveItem(itemCurrentlySelected);
            RefreshContent();
        }
        else
        {
            Debug.LogError("Equipment : " + itemCurrentlySelected.name + " non existant dans la librairie d'équipements");
        }
        CloseActionPanel();
    }

    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab, dropPoint.position, dropPoint.rotation);
        RemoveItem(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }

    public void DestroyActionButton()
    {
        RemoveItem(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }
}
