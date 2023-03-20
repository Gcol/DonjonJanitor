using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public StatDisplay[] statDisplayArray;
    public XMLManager mySave;
    public List<StatEntry> allStat;

    void Start()
    {
        allStat = mySave.LoadStat();

        //AddTestValue();
        UpdateDisplay();
    }

    void SaveChanges()
    {
        mySave.SaveStat(allStat);
    }

    void AddTestValue()
    {
        mySave.UpdateStat(allStat, "DonjonClean", 400);
        mySave.UpdateStat(allStat, "DonjonClean", 1050);
        mySave.UpdateStat(allStat, "BodyEliminate", 20);
        mySave.UpdateStat(allStat, "BloodClean", 10);
        mySave.UpdateStat(allStat, "JanitorDead", 199);

        SaveChanges();
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

}