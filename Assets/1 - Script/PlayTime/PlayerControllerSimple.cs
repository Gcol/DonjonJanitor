using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerSimple : MonoBehaviour
{
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    public bool playerActive = true;

    public GameObject littleBlood;
    public GameObject stepBlood;


    float timeBeforeBlood = 5f;
    float timeLittleStep = 2f;

    public float littleBloodTimer;
    public float littleStepTimer;

    public bool activeBloodStep;

    Rigidbody2D rbody;


    public void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponent<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            Debug.Log("Quit");
            quitFonction();

        }

        if (playerActive)
        {
            Vector2 currentPos = rbody.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            if (activeBloodStep)
            {
                littleStepTimer -= Time.deltaTime;
                if (littleStepTimer <= 0f)
                {
                    Instantiate(littleBlood, gameObject.transform.position, gameObject.transform.rotation);
                    littleStepTimer = timeLittleStep;
                }
            }
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
            rbody.MovePosition(newPos);
        }
    }
    public void quitFonction()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadScene(int sceneANumber)
    {
        SceneManager.LoadScene(sceneANumber);
    }

}
