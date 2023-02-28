using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    int sceneNum;
    public GameObject gameOver;
    public GameObject levelWin;

    public GameObject player;
    public GameObject bombSpawner;
    private AudioSource _audioSource;

    public GameObject defuseList;

    bool checkRestart = false;
    bool checkNext = false;

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
        levelWin = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        defuseList = GameObject.Find("Defuse List");
        started = false;
        ended = false;
        checkNext = false;
    }

    public void StartLevel()
    {
        if(!started)
        {
            checkNext = false;
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
            player.GetComponent<PlayerMovement>().GameOver();
            bombSpawner.GetComponent<BombSpawner>().GameOver();
            checkRestart = true;
        }
    }

    public void WinLevel()
    {
        if(!ended)
        {
            ended = true;
            levelWin = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
            levelWin.SetActive(true);
            player = GameObject.Find("Player");
            player.GetComponent<PlayerMovement>().WinLevel();
            checkNext = true;
            defuseList = GameObject.Find("Defuse List");
            defuseList.GetComponent<DefuseList>().Win();
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
            if(Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
            {
                checkRestart = false;
                RestartScene();
            }
        }
        if (checkNext)
        {
            if(Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
            {
                checkNext = false;
                NextScene();
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
        started = false;
        ended = false;
        checkNext = false;
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
