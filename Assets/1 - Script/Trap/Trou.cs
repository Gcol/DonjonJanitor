using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trou : MonoBehaviour
{
    public GameLogic gameCrtl;

    void OnCollisionEnter2D(Collision2D collision)
    {        
	  if (collision.gameObject.tag == "Player")
        {
            gameCrtl.AddStat("JanitorDead", 10);
            gameCrtl.RespawnPlayer();
        }

    else
        {
		    Destroy(collision.gameObject);
            gameCrtl.nbBody -= 1;
            gameCrtl.AddStat("BodyEliminate", 10); // as voir pour l'xp de supression (peux être en fonction de l'ennemie)
            gameCrtl.UpdateMissionTask();

        }
    }
}
