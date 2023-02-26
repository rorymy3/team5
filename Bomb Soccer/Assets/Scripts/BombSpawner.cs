using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public int bombCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        bombCount = gameObject.transform.childCount;
    }

    // Start is called before the first frame update
    public void StartLevel()
    {
        for(int i = 0; i < bombCount; i++)
        {
            Bomb curr = gameObject.transform.GetChild(i).GetComponent<Bomb>();
            curr.StartLevel();
        }
    }

    // Start is called before the first frame update
    public void GameOver()
    {
        for(int i = 0; i < bombCount; i++)
        {
            Bomb curr = gameObject.transform.GetChild(i).GetComponent<Bomb>();
            curr.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bombCount = gameObject.transform.childCount;
    }
}
