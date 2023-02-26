using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
       _audioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) {
      if (other.CompareTag("Bomb"))
      {
          _audioSource.Play();
      }
    }

    // void OnCollisionEnter2D(Collider2D other) {
    //   if (other.CompareTag("Bomb"))
    //   {
    //       _audioSource.Play();
    //   }
    // }

}
