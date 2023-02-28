using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defuse : MonoBehaviour
{
    public AudioManager am;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    public void WinLevel()
    {
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        explosion.Play();
        am.Play("Success");
        yield return new WaitForSeconds(9f);
        explosion.Pause();
    }
}
