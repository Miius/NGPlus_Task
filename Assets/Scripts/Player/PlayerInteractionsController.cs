using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionsController : MonoBehaviour
{
    private bool isNearReceiver = false;
    private ItemReceiver receiverNearby;

    private bool hasACollidingItem = false;
    private ItemPickup currentItem;

    [SerializeField] private GameObject hatObject;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            TryUseConsumable();
    }

    public void OnInteract(InputValue value)
    {
        // pegar item no chão
        if (hasACollidingItem && currentItem != null)
        {
            if (!InventoryController.Instance.CheckInventoryCapacity())
            {
                ClearItemData();
                return;
            }

            InventoryController.Instance.AddItem(currentItem.ItemData);
            currentItem.Collect(); // marca no save
            Destroy(currentItem.gameObject);
            ClearItemData();
            return;
        }

        // entregar itens ao receiver (apenas non-consumable)
        if (isNearReceiver && receiverNearby != null)
        {
            var allItems = InventoryController.Instance.GetItems();
            var interactiveItems = allItems.FindAll(i => i.type == ItemData.ItemType.InteractiveItem);

            if (interactiveItems.Count > 0)
            {
                receiverNearby.ReceiveItems(interactiveItems);
                InventoryController.Instance.RemoveSpecificItems(interactiveItems);
            }
        }
    }

    void TryUseConsumable()
    {
        ItemData consumable = InventoryController.Instance.GetFirstConsumable();
        if (consumable == null) return;

        if (hatObject != null) hatObject.SetActive(true);

        SaveManager.Instance.ConsumeItem(consumable.name);
        InventoryController.Instance.RemoveItem(consumable);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out ItemPickup itemPickup))
        {
            hasACollidingItem = true;
            currentItem = itemPickup;
        }

        if (other.TryGetComponent<ItemReceiver>(out ItemReceiver receiver))
        {
            isNearReceiver = true;
            receiverNearby = receiver;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out ItemPickup _))
            ClearItemData();

        if (other.TryGetComponent<ItemReceiver>(out ItemReceiver _))
        {
            isNearReceiver = false;
            receiverNearby = null;
        }
    }

    void ClearItemData()
    {
        hasACollidingItem = false;
        currentItem = null;
    }
}
