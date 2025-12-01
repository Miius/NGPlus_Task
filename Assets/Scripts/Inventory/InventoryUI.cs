using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        var inv = InventoryController.Instance.GetItems();

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inv.Count)
                slots[i].SetItem(inv[i]);
            else
                slots[i].Clear();
        }
    }
}
