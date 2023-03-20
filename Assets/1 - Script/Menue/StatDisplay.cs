using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text levelText;
    public GameObject Image;

    // Gestion de la barre d'xp
    public Slider xpSlider;
    public Gradient gradientXp;
    public Image XpBar;


    public void DisplayStat(string type, int level, int xp)
    {
        nameText.text = type;
        levelText.text = "Lvl : " + level.ToString();
        xpSlider.maxValue = (level + 1) * 100;
        xpSlider.value = xp;
        XpBar.color = gradientXp.Evaluate(xpSlider.normalizedValue);
    }
    public void HideEntryDisplay()
    {
        nameText.text = "? ? ?";
        levelText.text = "0";
        xpSlider.value = 0;
    }
}