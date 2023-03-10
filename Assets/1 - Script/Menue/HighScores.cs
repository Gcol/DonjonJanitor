using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplayArray;
    public XMLManager mySave;
    public List<HighScoreEntry> scores;
    void Start()
    {
        // Adds some test data
        scores = mySave.LoadScores();

        AddNewScore("John", 20f);
        AddNewScore("Max", 30f);
        AddNewScore("Dave", 40f);
        AddNewScore("Steve", 50f);
        AddNewScore("Mike", 60f);
        AddNewScore("Teddy", 70f);
        UpdateDisplay();

        //mySave.SaveScores(scores);

    }
    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry y, HighScoreEntry x) => y.temps.CompareTo(x.temps));
        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].temps);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }
    void AddNewScore(string entryName, float entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, temps = entryScore });
    }
}