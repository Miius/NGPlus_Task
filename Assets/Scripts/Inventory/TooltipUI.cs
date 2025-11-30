using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    private static TooltipUI instance;
    public static TooltipUI Instance => instance ? instance : FindFirstObjectByType<TooltipUI>();

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject tooltipObj;

    private RectTransform rectTransform;

    private bool isVisible = false;

    private void Awake()
    {
        rectTransform = tooltipObj.GetComponent<RectTransform>();

        var img = tooltipObj.GetComponent<UnityEngine.UI.Image>();
        if (img != null) img.raycastTarget = false;

        Hide();
    }

    private void Update()
    {
        if (!isVisible) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 tooltipXPos = new Vector2(mousePos.x, rectTransform.position.y);
        rectTransform.position = tooltipXPos;
    }

    public void Show(ItemData item)
    {
        if (isVisible) return;

        itemNameText.text = item.name;
        itemDescriptionText.text = item.description;

        tooltipObj.SetActive(true);
        isVisible = true;
    }

    public void Hide()
    {
        if (!isVisible) return; 

        tooltipObj.SetActive(false);
        isVisible = false;
    }
}
