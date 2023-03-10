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


    public TMP_Text timeWin;

    // Start is called before the first frame update
    void Start()
    {
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
            // Le joueur à gagné
            stopGame();
            UpdateTimer(timeWin);
            winPannel.SetActive(true);
        }
    }

    public void Restart()
    {
        RestartLevel();
        timeLeft = maxTimeLeft;

        winPannel.SetActive(false);
        pausePannel.SetActive(false);
        gameOverPannel.SetActive(false);

        lose = false;
        UpdateTimer(timer);
        player.GetComponent<Rigidbody2D>().position = spawn.position;

        resumeGame();
    }

    public void RestartLevel()
    {
        nbTask = 0;
        nbBody = 0;
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


    void FixedUpdate()
    {
        if (timerPlay)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timer);
        }

        if (timeLeft < 0)
        {
            stopGame();
            gameOverPannel.SetActive(true);
            lose = true;
        }

    }

    void UpdateTimer(TMP_Text currentText)
    {
        int minutes = Mathf.FloorToInt((timeLeft + 1) / 60);
        int secondes = Mathf.FloorToInt((timeLeft + 1) % 60);

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
}
