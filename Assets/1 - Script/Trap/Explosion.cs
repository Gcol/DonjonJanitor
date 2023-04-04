using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Trap
{
    public Sprite cleanTile;
    public Sprite exploseTile;

    void Awake()
    {
        deadType = "Explosion";
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (activeTrap)
        {
            GetComponent<SpriteRenderer>().sprite = exploseTile;
            activeTrap = false;
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();

            gameObject.tag = "ToCleanObject";
            gameCrtl.maxNbTask += 1;
        }
    }

    void Update()
    {
        if (!GetComponent<PolygonCollider2D>().isTrigger && !activeTrap)
        {
            // doit on changer ca ? Regarde plus tard 
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
}