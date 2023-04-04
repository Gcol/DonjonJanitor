using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text levelText;
    public Image imageType;
    public Sprite hideSprite;

    // Gestion de la barre d'xp
    public Slider xpSlider;
    public Gradient gradientXp;
    public Image XpBar;

    public Sprite defaultSprite;
    public Vector2 currentSizeDelta;

    public void Start()
    {
        HideEntryDisplay();
    }


    public void DisplayStat(string type, int level, int xp)
    {

        imageType.GetComponent<RectTransform>().sizeDelta = currentSizeDelta;
        imageType.sprite = defaultSprite;
        nameText.text = type;
        levelText.text = "Lvl : " + level.ToString();
        xpSlider.maxValue = (level + 1) * 100;
        xpSlider.value = xp;
        XpBar.color = gradientXp.Evaluate(xpSlider.normalizedValue);
    }

    public void HideEntryDisplay()
    {
        defaultSprite = imageType.sprite;

        imageType.sprite = hideSprite;
        currentSizeDelta = imageType.GetComponent<RectTransform>().sizeDelta;
        imageType.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
        nameText.text = "? ? ?";
        levelText.text = "0";
        xpSlider.value = 0;
    }
}