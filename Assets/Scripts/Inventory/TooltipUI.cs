using UnityEngine;
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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        rectTransform.position = Input.mousePosition;
    }

    public void Show(ItemData item)
    {
        itemNameText.text = item.name;
        itemDescriptionText.text = item.description;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
