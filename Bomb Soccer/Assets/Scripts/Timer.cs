using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LevelCountdown()
    {
        //Three
        yield return new WaitForSeconds(1f);
    }
}
