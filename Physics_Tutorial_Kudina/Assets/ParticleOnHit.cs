using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UIElements;

public class ParticleOnHit : MonoBehaviour

{
    public GameObject particle;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject NewParticle = Instantiate(particle, transform.position,
                    transform.rotation);
        StartCoroutine(despawnCoroutine(NewParticle));

    }
    IEnumerator despawnCoroutine(GameObject c)
    {
        yield return new WaitForSeconds(.2f);
        Destroy(c);
    }
}
