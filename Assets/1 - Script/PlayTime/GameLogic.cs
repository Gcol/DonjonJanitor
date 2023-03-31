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


    // Gestion des mouvements
    public GameObject player;


    public PlayerController playerCtr;

    //Gestion des taches à effectuer
    private GameObject[] spawnerDirtyList;
    private GameObject[] spawnerBodyList;
    private GameObject[] spawnerExplosionList;

    public GameObject[] dirtyPrefabList;
    public GameObject[] monsterPrefabList;
    public GameObject explosionPrefab;


    public int maxNbTask = 7;
    public int maxNbBody = 3;
    public int difficulty = 1;
    public int nbTask;
    public int nbBody;
    public TMP_Text nbTaskText;
    public TMP_Text nbBodyText;


    // Gestion de la victoire
    public List<StatEntry> allStat;
    public TMP_Text timeWin;
    public XMLManager mySave;


    //Animation de mort
    public Animator anim;
    public Animator deadJanitorAnim;

    // Start is called before the first frame update
    void Start()
    {
        spawnerBodyList = GameObject.FindGameObjectsWithTag("SpawnerBody");
        spawnerDirtyList = GameObject.FindGameObjectsWithTag("SpawnerDirtyThing");
        spawnerExplosionList = GameObject.FindGameObjectsWithTag("SpawnerExplosion");

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
            AddStat("DonjonClean", 1);

            mySave.SaveStat(allStat);
            winPannel.SetActive(true);
        }
    }

    public void AddStat(string name, int xpGain)
    {
        mySave.UpdateStat(allStat, name, xpGain);
    }

    public void Restart()
    {
        RestartLevel();
        allStat = mySave.LoadStat();
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

        // Ajout d'un premier test de diff

        int currentNbTask = Random.Range(difficulty * 2, difficulty * 3);
        int currentNbBody = Random.Range(difficulty, (difficulty + 1));

        Shuffle(spawnerDirtyList);
        Shuffle(spawnerBodyList);

        foreach (GameObject respawn in spawnerDirtyList)
        {
            Instantiate(dirtyPrefabList[Random.Range(0, dirtyPrefabList.Length)], respawn.transform.position, respawn.transform.rotation);
            if (nbTask >= currentNbTask) { break; }
        }

        foreach (GameObject respawn in spawnerBodyList)
        {
            nbBody += 1;
            Instantiate(monsterPrefabList[Random.Range(0, monsterPrefabList.Length)], respawn.transform.position, respawn.transform.rotation);
            if (nbBody >= currentNbBody) { break; }
        }

        foreach (GameObject respawn in spawnerExplosionList)
        {
            Instantiate(explosionPrefab, respawn.transform.position, respawn.transform.rotation);
        }

        UpdateMissionTask();
        // peux être joué avec deux liste ?
    }

    void CleanLevel()
    {
        DestroyFromTag("ToCleanObject");
        DestroyFromTag("MovingObject");

        GameObject[] objectToReset;

        objectToReset = GameObject.FindGameObjectsWithTag("NeedToCleanIfActivate");
        foreach (GameObject currentObject in objectToReset)
        {
            currentObject.GetComponent<TrouCaché>().Reset();
        }
    }

    void DestroyFromTag(string tag)
    {
        GameObject[] objetToDestroy;

        //Debug.Log("Destroy From Tag");
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

            mySave.SaveStat(allStat);
            gameOverPannel.SetActive(true);
            lose = true;
        }

    }

    void UpdateTimer(TMP_Text currentText, float timeToUpdate)
    {
        int minutes = Mathf.FloorToInt((timeToUpdate + 1) / 60);
        int secondes = Mathf.FloorToInt((timeToUpdate + 1) % 60);

        if (currentText == timeWin)
        {
            currentText.text = string.Format("You have win in : {0 : 00} : {1 : 00}", minutes, secondes);
        }
        else
        {
            currentText.text = string.Format("{0 : 00} : {1 : 00}", minutes, secondes);
        }
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

    public void RespawnPlayer(string animationDeadBody)
    {
        StartCoroutine(CoRoutineRespawnPlayer(animationDeadBody));
    }

    IEnumerator CoRoutineRespawnPlayer(string animationDeadBody)
    {
        playerCtr.playerActive = false;
        fadePannel.SetActive(true);
        anim.Play("FadeOurt");
        deadJanitorAnim.Play(animationDeadBody);

        yield return new WaitForSeconds(1.5f);
        player.GetComponent<Rigidbody2D>().position = spawn.position;
        yield return new WaitForSeconds(1f);

        fadePannel.SetActive(false);
        playerCtr.playerActive = true;
    }

}
