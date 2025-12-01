using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    public ItemData ItemData => itemData;

    [SerializeField] private string itemID;

    private void Start()
    {
        if (SaveManager.Instance != null && SaveManager.Instance.IsItemCollected(itemID))
            gameObject.SetActive(false);
    }

    public void Collect()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.CollectSceneItem(itemID);
    }
}
