using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    #region Singleton
    private static InventoryController instance;
    public static InventoryController Instance => instance ? instance : FindFirstObjectByType<InventoryController>();
    #endregion

    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] int inventoryMaxCapacity = 6;

    private int usedSlotCount = 0;
    private List<ItemData> items = new List<ItemData>();

    public bool CheckInventoryCapacity()
    {
        return usedSlotCount < inventoryMaxCapacity;
    }

    public void AddItem(ItemData itemToAdd)
    {
        if (!CheckInventoryCapacity()) return;

        items.Add(itemToAdd);
        usedSlotCount++;

        inventoryUI.RefreshUI();
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
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
        inventoryUI.RefreshUI();
    }
}
