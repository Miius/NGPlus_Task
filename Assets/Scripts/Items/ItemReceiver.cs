using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : MonoBehaviour
{
    [SerializeField] private int itemsCountToMakeCatapult = 9;
    private int itemsCount = 0;

    [SerializeField] private GameObject allItems;

    private void Start()
    {
        itemsCount = SaveManager.Instance != null ? SaveManager.Instance.GetReceiverCount() : 0;

        if (allItems != null)
            allItems.SetActive(itemsCount >= itemsCountToMakeCatapult);
    }

    public void ReceiveItems(List<ItemData> items)
    {
        itemsCount += items.Count;

        if (allItems != null && itemsCount >= itemsCountToMakeCatapult)
          {  allItems.SetActive(true);

          UIManager.Instance.ShowGameOver();
          }

        if (SaveManager.Instance != null)
            SaveManager.Instance.AddReceiverCount(items.Count);
    }
}
