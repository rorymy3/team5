using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    int sceneNum;
    public GameObject gameOver;

    public GameObject player;
    public GameObject bombSpawner;

    bool checkRestart = false;

    bool started = false;
    bool ended = false;

    void Awake()
    {
        if (manager == null) {
            manager = this;
            DontDestroyOnLoad(this);
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sceneNum = 0;
        player = GameObject.Find("Player");
        bombSpawner = GameObject.Find("Bomb Spawner");
        gameOver = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        started = false;
        ended = false;
    }

    public void StartLevel()
    {
        if(!started)
        {
            player = GameObject.Find("Player");
            bombSpawner = GameObject.Find("Bomb Spawner");
            started = true;
            player.GetComponent<PlayerMovement>().StartLevel();
            bombSpawner.GetComponent<BombSpawner>().StartLevel();
        }
    }

    public void GameOver()
    {
        if(!ended)
        {
            ended = true;
            gameOver.SetActive(true);
            player.GetComponent<PlayerMovement>().GameOver();
            bombSpawner.GetComponent<BombSpawner>().GameOver();
            checkRestart = true;
        }
    }

    void Update()
    {
        if(checkRestart)
        {
            if(Input.GetButton("Fire1") || Input.GetKey("space"))
            {
                checkRestart = false;
                RestartScene();
            }
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(sceneNum);
        started = false;
        ended = false;
    }

    void NextScene()
    {
        sceneNum++;
        SceneManager.LoadScene(sceneNum);
    }
}
