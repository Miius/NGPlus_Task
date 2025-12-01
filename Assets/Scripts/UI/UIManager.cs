using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

 #region Singleton
    private static UIManager instance;
    public static UIManager Instance => instance ? instance : FindFirstObjectByType<UIManager>();
    #endregion
 [Header("Panels")]
 [SerializeField] GameObject tutorialPanel;
 [SerializeField] GameObject hudPanel;
 [SerializeField] GameObject configPanel;
 [SerializeField] GameObject gameOverPanel;

[Header("Buttons")]
[SerializeField] Button configButton;

    void OnEnable()
    {
        configButton.onClick.AddListener(ShowConfig);
        tutorialPanel.SetActive(true);
        configButton.gameObject.SetActive(false);
    }

void DisableAllPanels()
    {
         tutorialPanel.SetActive(false);
          configPanel.SetActive(false);
           hudPanel.SetActive(false);
            gameOverPanel.SetActive(false);
    }

    public void CloseTutorial()
    {
       DisableAllPanels();
        configButton.gameObject.SetActive(true);
        ShowHud();
    }
    public void ShowTutorial()
    {
        DisableAllPanels();
        tutorialPanel.SetActive(true);
        configButton.gameObject.SetActive(false);
    }
    public void ShowHud()
    {
         hudPanel.SetActive(true);
    }
    public void ShowConfig()
    {
        DisableAllPanels();
        configButton.gameObject.SetActive(false);
        configPanel.SetActive(true);
    }
     public void CloseConfig()
    {
         DisableAllPanels();
        configButton.gameObject.SetActive(true);
        ShowHud();
    }

     public void ShowGameOver()
    {
        DisableAllPanels();
        configButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
    }

     public void ResetGame()
    {
           DisableAllPanels();
       SaveManager.Instance.DeleteSaveAndReloadScene();
    }
}
