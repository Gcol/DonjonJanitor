using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPerso : MonoBehaviour
{
    public Rigidbody2D spawn;
	
    void OnCollisionEnter2D(Collision2D collision)
    {        
	  if (collision.gameObject.tag == "Player")
        {        
            collision.rigidbody.position = spawn.position;
        }

        else
        {
		Debug.Log(collision.gameObject.name);
		Destroy(collision.gameObject);    
        }


    }
}
