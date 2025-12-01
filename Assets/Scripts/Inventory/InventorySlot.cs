using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventorySlot : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    private ItemData currentItem;
    [SerializeField] private Image icon;

    private Transform originalParent;
    private bool pointerInside = false;

    private void Start()
    {
        if (icon != null)
            icon.raycastTarget = false;
    }

    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (icon == null) return;

        if (item == null)
        {
            Clear();
            return;
        }

        icon.enabled = true;
        icon.sprite = item.icon;
    }

    public void Clear()
    {
        currentItem = null;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public ItemData GetItem() => currentItem;
    public bool IsEmpty() => currentItem == null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        TooltipUI.Instance.Hide();
        pointerInside = false;

        originalParent = transform;

        if (icon != null)
        {
            icon.transform.SetParent(transform.root);
            icon.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (icon != null)
            icon.transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        if (icon != null)
        {
            icon.transform.SetParent(originalParent);
            icon.transform.localPosition = Vector3.zero;
            icon.raycastTarget = false;
        }

        if (eventData.pointerEnter != null)
        {
            var otherSlot = eventData.pointerEnter.GetComponent<InventorySlot>();
            if (otherSlot != null && otherSlot != this)
                SwapItems(otherSlot);
        }
    }

    private void SwapItems(InventorySlot other)
    {
        ItemData temp = other.currentItem;

        other.SetItem(currentItem);
        SetItem(temp);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem == null) return;
        if (pointerInside) return;

        pointerInside = true;
        TooltipUI.Instance.Show(currentItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!pointerInside) return;

        pointerInside = false;
        TooltipUI.Instance.Hide();
    }
}
