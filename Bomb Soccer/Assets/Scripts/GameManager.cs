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
    private AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
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
            gameOver = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
            gameOver.SetActive(true);
            player = GameObject.Find("Player");
            bombSpawner = GameObject.Find("Bomb Spawner");
            bombSpawner = GameObject.Find("Bomb Spawner");
            player.GetComponent<PlayerMovement>().GameOver();
            bombSpawner.GetComponent<BombSpawner>().GameOver();
            checkRestart = true;
            //_audioSource.Play();
        }
    }

    void Update()
    {
        //delete this quit functionality when a Pause Menu is added
        if (Input.GetKey("escape")){
            QuitGame();
        }

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

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
