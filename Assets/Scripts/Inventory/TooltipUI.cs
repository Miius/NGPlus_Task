using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    #region Singleton
    private static TooltipUI instance;
    public static TooltipUI Instance => instance ? instance : FindFirstObjectByType<TooltipUI>();
    #endregion

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;

    private RectTransform rectTransform;
    [SerializeField] GameObject tooltipImg;

    private void Awake()
    {
        rectTransform = tooltipImg.GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        rectTransform.position = mousePos;
    }

    public void Show(ItemData item)
    {
        itemNameText.text = item.name;
        itemDescriptionText.text = item.description;

        tooltipImg.SetActive(true);
    }

    public void Hide()
    {
        tooltipImg.SetActive(false);
    }
}
