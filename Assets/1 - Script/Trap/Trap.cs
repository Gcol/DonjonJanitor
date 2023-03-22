using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trap : MonoBehaviour
{
    public GameLogic gameCrtl;
    public string deadType;
    public bool activeTrap = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (activeTrap)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameCrtl.AddStat("JanitorDead", 10);
                gameCrtl.RespawnPlayer(deadType);
            }
            else
            {
                if (collision.gameObject == gameCrtl.playerCtr.linkObject)
                {
                    gameCrtl.playerCtr.linkObject = null;
                }
                collision.gameObject.GetComponent<Animator>().Play(deadType);
                StartCoroutine(Destroytimer(collision));
                gameCrtl.nbBody -= 1;
                gameCrtl.AddStat("BodyEliminate", 10); // as voir pour l'xp de supression (peux �tre en fonction de l'ennemie)
                gameCrtl.UpdateMissionTask();

            }
        }
    }

    IEnumerator Destroytimer(Collision2D collision)
    {

        yield return new WaitForSeconds(1.5f);

        Destroy(collision.gameObject);

    }
}
