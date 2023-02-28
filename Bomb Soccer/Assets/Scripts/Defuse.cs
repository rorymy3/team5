using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defuse : MonoBehaviour
{
    public AudioManager am;
    public AudioManager mm;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        mm = GameObject.Find("Music Manager").GetComponent<AudioManager>();
    }

    public void WinLevel()
    {
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        explosion.Play();
        am.Play("Success");
        mm.Stop("Timer");
        mm.Stop("Skip Timer");
        mm.Play("End");
        yield return new WaitForSeconds(9f);
        explosion.Pause();
    }
}
