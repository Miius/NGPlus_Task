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
    int usedSlotCount = 0;

    List<ItemData> items = new List<ItemData>();

    public bool CheckInventoryCapacity(){
        if(usedSlotCount < inventoryMaxCapacity) return true;
        return false;
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
