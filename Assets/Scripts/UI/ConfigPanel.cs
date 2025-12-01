using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button tutorialButton; 
    [SerializeField] Button musicButton;
     [SerializeField] Button resetButton;

     [SerializeField] Sprite musicOnSprite, musicOffSprite;
     Sprite currentMusicSprite;
    void Awake()
    {
        continueButton.onClick.AddListener(Close);
        tutorialButton.onClick.AddListener(ShowTutorial);
        musicButton.onClick.AddListener(ChangeMusicStatus);
        resetButton.onClick.AddListener(ResetGame);
    }

    void OnEnable()
    {
         musicButton.image.sprite = AudioManager.Instance.IsMuted() ? musicOffSprite : musicOnSprite;
    }
    public void Close()
    {
       UIManager.Instance.CloseConfig();
    }

     public void ShowTutorial()
    {
       UIManager.Instance.ShowTutorial();
    }

     public void ResetGame()
    {
        UIManager.Instance.ResetGame();
    }

    public void ChangeMusicStatus()
    {
        AudioManager.Instance.ToggleMute();
        musicButton.image.sprite = AudioManager.Instance.IsMuted() ? musicOffSprite : musicOnSprite;
    }
}
