using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    int sceneNum;

    public GameObject player;
    public GameObject bombSpawner;

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
    }

    public void StartLevel()
    {
        player.GetComponent<PlayerMovement>().StartLevel();
        bombSpawner.GetComponent<BombSpawner>().StartLevel();
    }

    void NextScene()
    {
        sceneNum++;
        SceneManager.LoadScene(sceneNum);
    }
}
