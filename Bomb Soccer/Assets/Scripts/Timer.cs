using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject three;
    public GameObject two;
    public GameObject one;

    public GameManager gm;

    private Coroutine cd;

    // Start is called before the first frame update
    void Awake()
    {
        cd = StartCoroutine(LevelCountdown());
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButton("Fire1"))
        {
            StopCoroutine(cd);
            three.SetActive(false);
            two.SetActive(false);
            one.SetActive(false);
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
        Debug.Log("Before gm find");
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Debug.Log("After gm find");
        gm.StartLevel();
        Debug.Log("After gm call");
    }
}
