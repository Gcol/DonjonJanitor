using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text scoreText;
    public void DisplayHighScore(string name, float time)
    {
        int minutes = Mathf.FloorToInt((time + 1) / 60);
        int secondes = Mathf.FloorToInt((time + 1) % 60);

        nameText.text = name;
        scoreText.text = string.Format("{0 : 00} : {1 : 00}", minutes, secondes);
    }
    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
}