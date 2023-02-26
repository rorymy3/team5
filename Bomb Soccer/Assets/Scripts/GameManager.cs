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

    bool started = false;

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
    }

    public void StartLevel()
    {
        if(!started)
        {
            started = true;
            player.GetComponent<PlayerMovement>().StartLevel();
            bombSpawner.GetComponent<BombSpawner>().StartLevel();
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        player.GetComponent<PlayerMovement>().GameOver();
        bombSpawner.GetComponent<BombSpawner>().GameOver();
    }

    void NextScene()
    {
        sceneNum++;
        SceneManager.LoadScene(sceneNum);
    }
}
