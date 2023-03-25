using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrouCaché : Trap
{
    public Sprite cleanTile;

    void Awake()
    {
        deadType = "Chute";
    }

    public void Reset()
    {
        Color currentcolor = GetComponent<SpriteRenderer>().color;
        currentcolor.a = 1f;
        GetComponent<SpriteRenderer>().color = currentcolor;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (activeTrap)
        {
            Color currentcolor = GetComponent<SpriteRenderer>().color;
            currentcolor.a = 0f;
            GetComponent<SpriteRenderer>().color = currentcolor;
        }
    }
}