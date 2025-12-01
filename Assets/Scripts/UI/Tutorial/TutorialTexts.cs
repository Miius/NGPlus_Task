using UnityEngine;

[CreateAssetMenu(fileName = "TutorialTexts", menuName = "Scriptable Objects/TutorialTexts")]
public class TutorialTexts : ScriptableObject
{
     [TextArea(3, 10)]
    public string text;
}
