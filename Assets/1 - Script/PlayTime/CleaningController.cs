using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningController : MonoBehaviour
{
    public int healthBar = 255; // Voir comment on veux gerer ca ?
    public Color ImageColor;
    public GameLogic gameCrtl;

    // Start is called before the first frame update
    void Start()
    {

        SpriteRenderer cur_object = GetComponent<SpriteRenderer>();
        ImageColor = cur_object.color;
        gameCrtl = GameObject.FindWithTag("GameController").GetComponent<GameLogic>();

        gameCrtl.nbTask += 1;
        gameCrtl.UpdateMissionTask();
    }


    public void Decrease()
    {
        ImageColor.a -= 0.01f;

        // Changer par pourcentage sur la vie (comment faire d'une stat un %)
        if (ImageColor.a > 0 )
        {
            SpriteRenderer cur_object = GetComponent<SpriteRenderer>();
            cur_object.color = ImageColor;
        }
        else
        {
            //Debug.Log("Cleab Destroy");
            Destroy(gameObject);
            gameCrtl.nbTask -= 1;
            gameCrtl.AddStat("BloodClean", 1);
            gameCrtl.UpdateMissionTask();
            gameCrtl.playerCtr.activeBloodStep = false;
        }
    }

}
