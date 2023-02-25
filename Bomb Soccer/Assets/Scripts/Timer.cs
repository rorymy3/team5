using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject three;
    public GameObject two;
    public GameObject one;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelCountdown());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButton("Fire1"))
        {
            StopCoroutine(LevelCountdown());
            three.SetActive(false);
            two.SetActive(false);
            one.SetActive(false);
            //GameManager.StartLevel();
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
        //GameManager.StartLevel();
    }
}
