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

    private int usedSlotCount = 0;
    private List<ItemData> items = new List<ItemData>();

    private void Start()
    {
        LoadFromSave();
        inventoryUI.RefreshUI();
    }

    private void LoadFromSave()
    {
        items.Clear();
        usedSlotCount = 0;
        var saved = SaveManager.Instance.GetSavedInventory();

        foreach (var itemName in saved)
        {
            ItemData data = Resources.Load<ItemData>("Items/" + itemName);
            if (data != null)
            {
                items.Add(data);
                usedSlotCount++;
            }
        }
    }

    public bool CheckInventoryCapacity()
    {
        return usedSlotCount < inventoryMaxCapacity;
    }

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
            usedSlotCount--;
            SaveManager.Instance.RemoveInventoryItem(itemToRemove.name);
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

    public ItemData GetFirstConsumable()
    {
        return items.Find(i => i.type == ItemData.ItemType.ConsumableItem);
    }

    public List<ItemData> GetItems()
    {
        return items;
    }

    public void ClearInventory()
    {
        items.Clear();
        usedSlotCount = 0;
        SaveManager.Instance.ClearInventorySave();
        inventoryUI.RefreshUI();
    }
}
