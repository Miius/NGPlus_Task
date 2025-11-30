using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionsController : MonoBehaviour
{
    private bool isNearReceiver = false;
    private ItemReceiver receiverNearby;

    private bool hasACollidingItem = false;
    private ItemPickup currentItem;

    [SerializeField] GameObject hatObject;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            TryUseConsumable();
    }

    public void OnInteract(InputValue value)
    {
        if (hasACollidingItem)
        {
            bool hasInventorySpace = InventoryController.Instance.CheckInventoryCapacity();

            if (!hasInventorySpace)
            {
                ClearItemData();
                return;
            }

            InventoryController.Instance.AddItem(currentItem.ItemData);
            Destroy(currentItem.gameObject);
            ClearItemData();
            return;
        }

        if (isNearReceiver)
        {
            var allItems = InventoryController.Instance.GetItems();

            var interactiveItems = allItems.FindAll(
                i => i.type == ItemData.ItemType.InteractiveItem
            );

            receiverNearby.ReceiveItems(interactiveItems);

            InventoryController.Instance.RemoveSpecificItems(interactiveItems);
        }
    }

    void TryUseConsumable()
    {
        ItemData consumable = InventoryController.Instance.GetFirstConsumable();
        if (consumable == null) return;

       hatObject.SetActive(true);

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
