using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject sparks;
    public GameObject circle;
    public GameObject bombArt;
    public GameObject bombOverlay;
    public GameObject craterArt;
    public GameManager gm;
    public ParticleSystem explosion;
    public float time;
    private bool exploded = false;

    private Coroutine c;

    private Vector3 fullCircle;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        sparks.SetActive(true);
        fullCircle = circle.transform.localScale;
        circle.SetActive(false);
        exploded = false;
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartLevel()
    {
        c = StartCoroutine(Countdown());
    }

    public void GameOver()
    {
        if(!exploded)
        {
            StopCoroutine(c);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Defuse")
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
            Vector3 pos = col.gameObject.transform.position;
            StopCoroutine(c);
            StartCoroutine(Defuse(pos));
        }
    }

    IEnumerator Defuse(Vector3 pos)
    {
        sparks.SetActive(false);
        float scale = gameObject.transform.localScale.x;
        float shrink = scale / 30;
        Vector3 dist = pos - gameObject.transform.position;
        Vector3 move = dist / 30;
        for(int i = 0; i < 30; i++)
        {
            scale -= shrink;
            gameObject.transform.localScale = new Vector2(scale, scale);
            gameObject.transform.position += move;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Countdown()
    {
        float timeDiv = time / 100;
        circle.SetActive(true);
        circle.transform.localScale = new Vector3(0,0,0);
        Vector3 scaleChange = fullCircle / 100;
        for(int i = 0; i < 100; i++)
        {
            circle.transform.localScale += scaleChange;
            yield return new WaitForSeconds(timeDiv);
        }
        bombArt.SetActive(false);
        bombOverlay.SetActive(false);
        craterArt.SetActive(true);
        exploded = true;
        gm.GameOver();
        explosion.Play();
        yield return new WaitForSeconds(9f);
        explosion.Pause();
    }
}