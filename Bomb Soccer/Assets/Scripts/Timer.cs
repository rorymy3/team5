using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject three;
    public GameObject two;
    public GameObject one;

    public AudioManager mm;
    bool skipped = false;

    public static GameManager gm;

    private Coroutine cd;

    // Start is called before the first frame update
    void Start()
    {
        cd = StartCoroutine(LevelCountdown());
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            mm.Play("Timer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !skipped)
        {
            skipped = true;
            mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
            mm.Stop("Timer");
            if(SceneManager.GetActiveScene().name != "Tutorial")
            {
                mm.Play("Skip Timer");
            }
            StopCoroutine(cd);
            three.SetActive(false);
            two.SetActive(false);
            one.SetActive(false);
            gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gm.StartLevel();
        }
    }

    IEnumerator LevelCountdown()
    {
        three.SetActive(true);
        yield return new WaitForSeconds(1f);
        three.SetActive(false);
        two.SetActive(true);
        yield return new WaitForSeconds(1f);
        two.SetActive(false);
        one.SetActive(true);
        yield return new WaitForSeconds(1f);
        one.SetActive(false);
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gm.StartLevel();
        skipped = true;
    }
}
