using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    #region Singleton
    private static InventoryController instance;
    public static InventoryController Instance => instance ? instance : FindFirstObjectByType<InventoryController>();
    #endregion

    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int inventoryMaxCapacity = 6;

    [Header("All items in the game")]
    [SerializeField] private List<ItemData> allItems;

    private int usedSlotCount = 0;
    private List<ItemData> items = new List<ItemData>();

    private void Start()
    {
        LoadFromSave();
    }

    void LoadFromSave()
    {
        items.Clear();
        usedSlotCount = 0;

        if (SaveManager.Instance == null)
            return;

        var savedList = SaveManager.Instance.GetSavedInventory();

        foreach (string itemName in savedList)
        {
            ItemData loadedItem = allItems.Find(i => i.name == itemName);
            if (loadedItem != null)
            {
                items.Add(loadedItem);
                usedSlotCount++;
            }
        }

        inventoryUI.RefreshUI();
    }

    public bool CheckInventoryCapacity() => usedSlotCount < inventoryMaxCapacity;

    public void AddItem(ItemData itemToAdd)
    {
        if (!CheckInventoryCapacity() || itemToAdd == null) return;

        items.Add(itemToAdd);
        SaveManager.Instance.AddInventoryItem(itemToAdd.name);
        usedSlotCount++;
        inventoryUI.RefreshUI();
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        if (itemToRemove == null) return;
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            SaveManager.Instance.RemoveInventoryItem(itemToRemove.name);
            usedSlotCount--;
            inventoryUI.RefreshUI();
        }
    }

    public void RemoveSpecificItems(List<ItemData> toRemove)
    {
        foreach (var item in toRemove)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                usedSlotCount--;
                SaveManager.Instance.RemoveInventoryItem(item.name);
            }
        }
        inventoryUI.RefreshUI();
    }

    public ItemData GetFirstConsumable() => items.Find(i => i.type == ItemData.ItemType.ConsumableItem);

    public List<ItemData> GetItems() => items;

    public void ClearInventory()
    {
        items.Clear();
        usedSlotCount = 0;
        SaveManager.Instance.ClearInventorySave();
        inventoryUI.RefreshUI();
    }
}
