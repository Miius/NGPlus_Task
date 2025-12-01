using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Button resetButton;

    void Awake()
    {
        resetButton.onClick.AddListener(ResetGame);
    }
    public void ResetGame()
    {
        UIManager.Instance.ResetGame();
    }
}
