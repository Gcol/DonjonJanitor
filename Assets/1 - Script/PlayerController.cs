using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public GameObject linkObject = null;
    public CleaningController cleanObject = null;
    //IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    public void setCleanObject(CleaningController newCleanObject)
    {
        cleanObject = newCleanObject;
    }

    public void Awake()
    {
        linkObject = null;
        rbody = GetComponent<Rigidbody2D>();
        //isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector  = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        if (Input.GetAxis("Jump") > 0)
        {
            if (linkObject)
            {
                movement = inputVector * movementSpeed / 2;
                // On  ajout le poid de l'objet

                Vector2 previousPos = linkObject.GetComponent<Rigidbody2D>().position;
                linkObject.GetComponent<Rigidbody2D>().MovePosition(previousPos + movement * Time.fixedDeltaTime);
            }
            else if (cleanObject)
            {
                cleanObject.Decrease();
            }


        }
        else if (linkObject != null) { linkObject = null; }
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingObject")
        {
            linkObject = collision.gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ToCleanObject")
        {
            cleanObject = collision.gameObject.GetComponent<CleaningController>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<CleaningController>() == cleanObject)
        {
            cleanObject = null;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == linkObject){
            linkObject = null;
        }
    }

}
