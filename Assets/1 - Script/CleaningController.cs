using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningController : MonoBehaviour
{
    public int healthBar = 255;
    public Color ImageColor;

    // Start is called before the first frame update
    void Start()
    {

        SpriteRenderer cur_object = GetComponent<SpriteRenderer>();
        ImageColor = cur_object.color; 

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
        }
    }

}
