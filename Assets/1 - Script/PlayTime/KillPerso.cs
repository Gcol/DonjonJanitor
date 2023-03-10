using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPerso : MonoBehaviour
{
    public Rigidbody2D spawn;
    public GameLogic gameCrtl;

    void OnCollisionEnter2D(Collision2D collision)
    {        
	  if (collision.gameObject.tag == "Player")
        {        
            collision.rigidbody.position = spawn.position;
        }

    else
        {
		    Destroy(collision.gameObject);
            gameCrtl.nbBody -= 1;
            gameCrtl.UpdateMissionTask();
        }
    }
}
