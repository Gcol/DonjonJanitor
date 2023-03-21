using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    // Start is called before the first frame update


    public static readonly string[] staticDirection = { "Static N", "Static W", "Static S", "Static E" };
    public static readonly string[] runDirection = { "Run N", "Run W", "Run S", "Run E" };

    public float angle = 0f;

    Animator animator;
    int lastDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < 0.01f)
        {
            directionArray = staticDirection;
        }
        else
        {
            directionArray = runDirection;
            lastDirection = DirectionToIndex(direction, 4);
        }

        animator.Play(directionArray[lastDirection]);

    }

    public int DirectionToIndex(Vector2 dir, int sliceCount)
    {

        Vector2 normDir = dir.normalized;

        float step = 360f / sliceCount;

        float halfstep = step / 2;

        angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfstep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
