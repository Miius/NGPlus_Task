using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button closeButton; 
    [SerializeField] Button nextButton;
     [SerializeField] Button previousButton;

     [SerializeField] List<TutorialTexts> tutorialTexts = new List<TutorialTexts>();
     int textCount = 0;


    void Awake()
    {
         closeButton.onClick.AddListener(Close);
        nextButton.onClick.AddListener(Next);
        previousButton.onClick.AddListener(Previous);

    }
    void OnEnable()
    {
        textCount = 0;
       
        previousButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        text.text = tutorialTexts[textCount].text;
    }

    private void Next()
    {
        if(textCount < tutorialTexts.Count-1)
            textCount++;
        if(textCount == tutorialTexts.Count-1)
            nextButton.gameObject.SetActive(false);
        
         text.text = tutorialTexts[textCount].text;
         previousButton.gameObject.SetActive(true);
    }

    private void Previous()
    {
         if(textCount > 0)
            textCount--;

        if(textCount == 0)
            previousButton.gameObject.SetActive(false);

        text.text = tutorialTexts[textCount].text;
         nextButton.gameObject.SetActive(true);
    }

    private void Close()
    {
        UIManager.Instance.CloseTutorial();
    }
}
