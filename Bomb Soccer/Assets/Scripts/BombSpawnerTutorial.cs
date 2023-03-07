using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerTutorial : MonoBehaviour
{
    public int bombCount = 0;
    public int bombTotal = 0;
    public GameManager gm;
    bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        bombTotal = gameObject.transform.childCount;
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    public void StartLevel()
    {
        won = false;
    }

    // Start is called before the first frame update
    public void GameOver()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bombTotal = gameObject.transform.childCount;
        bombCount = 0;
        for(int i = 0; i < bombTotal; i++)
        {
            KickTutorial curr = gameObject.transform.GetChild(i).GetComponent<KickTutorial>();
            bombCount += curr.count;
        }
        if(bombCount <= 0 && !won)
        {
            won = true;
            gm.WinLevel();
        }
    }
}
