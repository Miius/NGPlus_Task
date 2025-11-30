using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InventorySlot :  MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    private ItemData currentItem;
    [SerializeField] private Image icon;

    private Transform originalParent;

    public void SetItem(ItemData item)
    {
        currentItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void Clear()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public ItemData GetItem() => currentItem;
    public bool IsEmpty() => currentItem == null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        originalParent = transform.parent;
        transform.SetParent(transform.root);
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        icon.raycastTarget = true;

        if (eventData.pointerEnter != null)
        {
            InventorySlot otherSlot = eventData.pointerEnter.GetComponent<InventorySlot>();

            if (otherSlot != null && otherSlot != this)
                SwapItems(otherSlot);
        }
    }

    private void SwapItems(InventorySlot other)
    {
        ItemData temp = other.currentItem;
        other.SetItem(this.currentItem);

        if (temp == null)
            this.Clear();
        else
            this.SetItem(temp);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem == null) return;
        TooltipUI.Instance.Show(currentItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.Hide();
    }
}
