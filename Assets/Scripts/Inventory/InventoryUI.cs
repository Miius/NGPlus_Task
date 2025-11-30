using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
     [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        var items = InventoryController.Instance.GetItems();

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
                slots[i].SetItem(items[i]);
            else
                slots[i].Clear();
        }
    }
}
