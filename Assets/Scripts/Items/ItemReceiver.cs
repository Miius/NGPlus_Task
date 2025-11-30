using UnityEngine;
using System.Collections.Generic;

public class ItemReceiver : MonoBehaviour
{
    [SerializeField] int itemsCountToMakeCatapult = 9;
    int itemsCount = 0;

    [SerializeField] GameObject allItems;
    public void ReceiveItems(List<ItemData> items)
    {
        foreach (var item in items)
        {
           itemsCount++;
        }

        if(itemsCount >= itemsCountToMakeCatapult)
            allItems.SetActive(true);
    }
}
