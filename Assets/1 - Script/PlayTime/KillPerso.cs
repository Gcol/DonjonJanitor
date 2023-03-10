using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPerso : MonoBehaviour
{
    public GameLogic gameCrtl;

    void OnCollisionEnter2D(Collision2D collision)
    {        
	  if (collision.gameObject.tag == "Player")
        {
            gameCrtl.RespawnPlayer();
        }

    else
        {
		    Destroy(collision.gameObject);
            gameCrtl.nbBody -= 1;
            gameCrtl.UpdateMissionTask();

        }
    }
}
