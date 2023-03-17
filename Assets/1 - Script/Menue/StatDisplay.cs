using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public GameObject Image;
    public GameObject xpBar;
    public void DisplayStat(string type, int level, int xp)
    {
        nameText.text = type;
        levelText.text = level.ToString();

    }
    public void HideEntryDisplay()
    {
        nameText.text = "";
        levelText.text = "";
    }
}