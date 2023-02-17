using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject sparks;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        sparks.SetActive(true);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Defuse")
        {
            StartCoroutine(Defuse());
        }
    }

    IEnumerator Defuse()
    {
        sparks.SetActive(false);
        float scale = gameObject.transform.localScale.x;
        float shrink = scale / 30;
        for(int i = 0; i < 30; i++)
        {
            scale -= shrink;
            gameObject.transform.localScale = new Vector2(scale, scale);
            yield return new WaitForSeconds(0.01f);
        }
    }
}