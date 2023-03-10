using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class GameLogic : MonoBehaviour
{
    // Respawn du joueur
    public Rigidbody2D spawn;

    //Gestion de la lose condition
    public TMP_Text timer;
    public float maxTimeLeft = 60f;
    public bool timerPlay = true;
    public bool lose;

    float timeLeft = 60f;


    // Gestion des canvas
    public GameObject gameOverPannel;
    public GameObject pausePannel;
    public GameObject winPannel;
    public GameObject fadePannel;


    public GameObject player;


    PlayerController playerCtr;

    //Gestion des taches à effectuer
    private GameObject[] spawnerDirtyList;
    private GameObject[] spawnerBodyList;

    public GameObject[] dirtyPrefabList;
    public GameObject[] monsterPrefabList;


    public int maxNbTask = 7;
    public int maxNbBody = 3;
    public int nbTask;
    public int nbBody;
    public TMP_Text nbTaskText;
    public TMP_Text nbBodyText;


    // Gestion de la victoire
    public List<HighScoreEntry> scores;
    public TMP_Text timeWin;
    public XMLManager mySave;
    public TMP_Text textAreaRegister;
    public GameObject registerArea; 

    //Animation de mort
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        scores = mySave.LoadScores();
        spawnerBodyList = GameObject.FindGameObjectsWithTag("SpawnerBody");
        spawnerDirtyList = GameObject.FindGameObjectsWithTag("SpawnerDirtyThing");

        playerCtr = player.GetComponent<PlayerController>();
        Restart();
    }

    public void UpdateMissionTask()
    {
        nbBodyText.text = nbBody.ToString();
        nbTaskText.text = nbTask.ToString();

        if (nbBody == 0 && nbTask == 0)
        {
            stopGame();
            UpdateTimer(timeWin, maxTimeLeft - timeLeft);
            registerArea.SetActive(true);
            winPannel.SetActive(true);
        }
    }

    public void RegisterNewScore()
    {
        AddNewScore(textAreaRegister.text, maxTimeLeft - timeLeft);
        Debug.Log("Add New sxcore");
        mySave.SaveScores(scores);
        registerArea.SetActive(false);
    }

    public void Restart()
    {
        RestartLevel();
        timeLeft = maxTimeLeft;

        winPannel.SetActive(false);
        pausePannel.SetActive(false);
        gameOverPannel.SetActive(false);

        lose = false;
        UpdateTimer(timer, timeLeft);
        player.GetComponent<Rigidbody2D>().position = spawn.position;

        resumeGame();
    }

    void RestartLevel()
    {
        nbTask = 0;
        nbBody = 0;

        CleanLevel();
        // As voir comment déterminer le max ?

        int currentNbTask = Random.Range(3, maxNbTask);
        int currentNbBody = Random.Range(1, maxNbBody);

        Shuffle(spawnerDirtyList);
        Shuffle(spawnerBodyList);

        foreach (GameObject respawn in spawnerDirtyList)
        {
            nbTask += 1;
            Instantiate(dirtyPrefabList[Random.Range(0, dirtyPrefabList.Length)], respawn.transform.position, respawn.transform.rotation);
            if (nbTask >= currentNbTask) { break; }
        }

        foreach (GameObject respawn in spawnerBodyList)
        {
            nbBody += 1;
            Instantiate(monsterPrefabList[Random.Range(0, monsterPrefabList.Length)], respawn.transform.position, respawn.transform.rotation);
            if (nbBody >= currentNbBody) { break; }
        }

        UpdateMissionTask();
        // peux être joué avec deux liste ?
    }

    void CleanLevel()
    {
        DestroyFromTag("ToCleanObject");
        DestroyFromTag("MovingObject");
    }

    void DestroyFromTag(string tag)
    {
        GameObject[] objetToDestroy;

        objetToDestroy = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject currentObject in objetToDestroy)
        {
            Destroy(currentObject);
        }

    }

    void FixedUpdate()
    {
        if (timerPlay)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timer, timeLeft);
        }

        if (timeLeft < 0)
        {
            stopGame();
            gameOverPannel.SetActive(true);
            lose = true;
        }

    }

    void UpdateTimer(TMP_Text currentText, float timeToUpdate)
    {
        int minutes = Mathf.FloorToInt((timeToUpdate + 1) / 60);
        int secondes = Mathf.FloorToInt((timeToUpdate + 1) % 60);

        currentText.text = string.Format("{0 : 00} : {1 : 00}", minutes, secondes);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel") && lose != true)
        {
            if (playerCtr.playerActive)
            {
                stopGame();
                pausePannel.SetActive(true);
            }
            else
            {
                resumeGame();
                pausePannel.SetActive(false);

            }
        }
    }

    void stopGame()
    {
        playerCtr.playerActive = false;
        timerPlay = false;
    }


    void resumeGame()
    {
        playerCtr.playerActive = true;
        timerPlay = true;
    }

    void Shuffle(GameObject[] listToShuffle)
    {
        for (int i = listToShuffle.Length - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);

            GameObject temp = listToShuffle[i];

            listToShuffle[i] = listToShuffle[rnd];
            listToShuffle[rnd] = temp;
        }
    }


    public void RespawnPlayer()
    {
        StartCoroutine(CoRoutineRespawnPlayer());
    }

    IEnumerator CoRoutineRespawnPlayer()
    {
        playerCtr.playerActive = false;
        fadePannel.SetActive(true);
        anim.Play("FadeOurt");

        yield return new WaitForSeconds(1.5f);
        player.GetComponent<Rigidbody2D>().position = spawn.position;
        yield return new WaitForSeconds(1f);

        fadePannel.SetActive(false);
        playerCtr.playerActive = true;
    }


    void AddNewScore(string entryName, float entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, temps = entryScore });
    }
}
