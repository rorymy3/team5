using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    int sceneNum;
    public GameObject gameOver;
    public GameObject levelWin;
    public AudioManager mm;
    public AudioManager am;

    public GameObject player;
    public GameObject bombSpawner;
    private AudioSource _audioSource;
    public GameObject defuseList;

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

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
        // _audioSource.outputAudioMixerGroup = FindObjectOfType<AudioMixerGroup>();
        // mixer = _audioSource.outputAudioMixerGroup.audioMixer;

        sceneNum = 0;
        player = GameObject.Find("Player");
        bombSpawner = GameObject.Find("Bomb Spawner");
        gameOver = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        levelWin = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
        defuseList = GameObject.Find("Defuse List");
        started = false;
        ended = false;
        checkNext = false;

        // pause menu stuff
        // AudioMixer mixer = GameObject.Find("PAUSEMENU");
        //pauseMenuUI = GameObject.FindWithTag("PAUSEMENU");
        //mixer = FindObjectOfType<AudioMixer>();
        pauseMenuUI = GameObject.Find("Canvas").transform.Find("PAUSEMENU").gameObject;
        pauseMenuUI.SetActive(false);
        GameisPaused = false;

        // Volume stuff
        SetVolumeLevel (volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
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
            if(SceneManager.GetActiveScene().name == "Tutorial")
            {
                bombSpawner.GetComponent<BombSpawnerTutorial>().StartLevel();
            }
            else
            {
                bombSpawner.GetComponent<BombSpawner>().StartLevel();
            }
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
            if(SceneManager.GetActiveScene().name == "Tutorial")
            {
                bombSpawner.GetComponent<BombSpawnerTutorial>().GameOver();
            }
            else
            {
                bombSpawner.GetComponent<BombSpawner>().GameOver();
            }
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
        if (Input.GetKeyDown(KeyCode.Escape)){
                if (GameisPaused){ Resume(); }
                else{ Pause(); }
        }

        if(checkRestart)
        {
            if(Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
            {
                checkRestart = false;
                mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
                mm.Stop("Death");
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
        if(Input.GetKeyDown("r"))
        {
            checkRestart = false;
            mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
            mm.Stop("Death");
            mm.Stop("Timer");
            mm.Stop("Skip Timer");
            RestartScene();
        }
    }

    void Pause(){
            pauseMenuUI = GameObject.Find("Canvas").transform.Find("PAUSEMENU").gameObject;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;

            mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
            mm.PauseAll();
            am = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
            am.PauseAll();
    }

    public void Resume(){
            pauseMenuUI = GameObject.Find("Canvas").transform.Find("PAUSEMENU").gameObject;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameisPaused = false;

            mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
            mm.PlayAll();
            am = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
            am.PlayAll();
    }

    public void SetVolumeLevel (float sliderValue){
            //mixer = FindObjectOfType<AudioMixer>();
            mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
            volumeLevel = sliderValue;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(sceneNum);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        started = false;
        ended = false;
    }

    void NextScene()
    {
        started = false;
        ended = false;
        checkNext = false;
        sceneNum++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
