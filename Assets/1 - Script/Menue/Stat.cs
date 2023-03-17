using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public StatDisplay[] statDisplayArray;
    public XMLManager mySave;
    public List<StatEntry> allStat;
    void Start()
    {
        // Adds some test data
        allStat = mySave.LoadStat();

        UpdateScore("DonjonClean", 400);
        UpdateScore("DonjonClean", 1050);
        UpdateScore("BodyEliminate", 20);
        UpdateScore("BloodClean", 10);
        UpdateScore("JanitorDead", 199);
        UpdateDisplay();

        //mySave.SaveScores(scores);

    }
    void UpdateDisplay()
    {
        for (int i = 0; i < statDisplayArray.Length; i++)
        {
            if (i < allStat.Count)
            {
                statDisplayArray[i].DisplayStat(allStat[i].type, allStat[i].level, allStat[i].xp);
            }
            else
            {
                statDisplayArray[i].HideEntryDisplay();
            }
        }
    }
    void UpdateScore(string entryName, int xpGain)
    {
        StatEntry currentStat = allStat.Find((x) => x.type == entryName);

        if (currentStat == null)
        {
            currentStat = new StatEntry { type = entryName, level = 0, xp = 0 };
            allStat.Add(currentStat);

        }

        currentStat.xp += xpGain;
        while (currentStat.xp >= (currentStat.level + 1 * 100))
        {
            currentStat.xp -= currentStat.level + 1 * 100;
            currentStat.level++;
        }
    }
}