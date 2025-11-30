using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionsController : MonoBehaviour
{
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
        }
    }

   private void OnTriggerEnter(Collider other)
{
    if(other.TryGetComponent<ItemPickup>(out ItemPickup itemPickup)){
        hasACollidingItem = true;
        currentItem = itemPickup;

    }
}

private void OnTriggerExit(Collider other)
{
   ClearItemData();
}

void ClearItemData(){
    hasACollidingItem = false;
            currentItem = null;
}
}
