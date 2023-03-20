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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decrease()
    {
        ImageColor.a -= 0.01f;
        if (ImageColor.a > 0 )
        {
            SpriteRenderer cur_object = GetComponent<SpriteRenderer>();
            cur_object.color = ImageColor;
        }
        else
        {
            Destroy(gameObject);
            gameCrtl.nbTask -= 1;
            gameCrtl.AddStat("BloodClean", healthBar);
            gameCrtl.UpdateMissionTask();
        }
    }

}
