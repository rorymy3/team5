using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    public AudioManager am;
    int step = 4;

    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    void Step()
    {
        if(step % 4 == 0)
        {
            am.Play("Step1");
        }
        else if(step % 4 == 1)
        {
            am.Play("Step2");
        }
        else if(step % 4 == 2)
        {
            am.Play("Step3");
        }
        else
        {
            am.Play("Step4");
        }
        step++;
    }
}
