using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpriteRenderedOnPlay : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
}
