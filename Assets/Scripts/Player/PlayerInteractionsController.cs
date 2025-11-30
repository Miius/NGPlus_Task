using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionsController : MonoBehaviour
{
    private bool isNearReceiver = false;
    private ItemReceiver receiverNearby;

    private bool hasACollidingItem = false;
    private ItemPickup currentItem;


    public void OnInteract(InputValue value)
    {
        if (hasACollidingItem)
        {
            bool hasInventorySpace = InventoryController.Instance.CheckInventoryCapacity();
            if(!hasInventorySpace){
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
            var items = InventoryController.Instance.GetItems();
            receiverNearby.ReceiveItems(items);

            InventoryController.Instance.ClearInventory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ItemPickup>(out ItemPickup itemPickup))
        {
            hasACollidingItem = true;
            currentItem = itemPickup;
        }

        if(other.TryGetComponent<ItemReceiver>(out ItemReceiver receiver))
        {
            isNearReceiver = true;
            receiverNearby = receiver;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemPickup>(out ItemPickup _))
            ClearItemData();

        if(other.TryGetComponent<ItemReceiver>(out ItemReceiver _))
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
