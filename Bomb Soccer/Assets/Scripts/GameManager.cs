using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEnginer.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManagement manager;
    int sceneNum;

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
    }

    void Update()
    {

    }

    void NextScene()
    {
        sceneNum++;
        SceneManager.LoadScene(sceneNum);
    }
}
