using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trap : MonoBehaviour
{   
    public GameLogic gameCrtl;
    public string deadType;
    public bool activeTrap = true;

    private Collision2D collisionToClean;

    void Start()
    {
        gameCrtl = GameObject.FindWithTag("GameController").GetComponent<GameLogic>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.LogFormat("Collision with {0} tag {1}", collision.gameObject.name, collision.gameObject.tag);

        if (activeTrap)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameCrtl.AddStat("JanitorDead", 10);
                gameCrtl.RespawnPlayer(deadType);
            }
            else
            {
                collisionToClean = collision;
                //Debug.LogFormat("New collision to erase {0}", collisionToClean.gameObject.name);
                if (collisionToClean.gameObject == gameCrtl.playerCtr.linkObject)
                {
                    gameCrtl.playerCtr.linkObject = null;
                }
                collisionToClean.gameObject.GetComponent<Animator>().Play(deadType);
                StartCoroutine(Destroytimer(collisionToClean.gameObject));
                gameCrtl.nbBody -= 1;
                gameCrtl.AddStat("BodyEliminate", 10); // as voir pour l'xp de supression (peux �tre en fonction de l'ennemie)
                gameCrtl.UpdateMissionTask();

            }
        }
    }

    IEnumerator Destroytimer(GameObject GameObjectToErase)
    {
        //Debug.LogFormat("Destroy from collision {0}", GameObjectToErase.name);
        yield return new WaitForSeconds(1.5f);
        //Debug.LogFormat("Destroy from collision {0}", GameObjectToErase.name);

        Destroy(GameObjectToErase);

    }
}
