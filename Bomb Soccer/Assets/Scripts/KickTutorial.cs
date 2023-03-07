using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KickTutorial : MonoBehaviour
{
    private Transform player;
    private Collider2D col;
    private Collider2D kickZone;
    public GameObject arrow;
    public SpriteRenderer arrowArt;
    public Transform arrowScaler;
    private Rigidbody2D rb;

    public float kickRadius = 3f;
    public int count = 1;
    private AudioSource _audioSource;

    bool ableToKick = false;
    bool buildKick = false;
    public bool canMove = true;
    float kickSpeed = 0f;
    int kickTime = 0;

    public Color off;
    public Color on;

    // distance between player and object.
    public Vector3 dist;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        player = GameObject.Find("Player").transform;
        kickZone = player.Find("Kick Zone").gameObject.GetComponent<Collider2D>();
        //arrow = transform.Find("Arrow").gameObject;
        //arrowArt = arrow.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        arrowArt.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource> ();
        arrowScaler.localScale = new Vector3(0.25f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            dist = transform.position - player.position;
            arrow.transform.right = -(player.position - transform.position);

            if(col.IsTouching(kickZone))
            {
                ableToKick = true;
                arrowArt.color = on;
            }
            else
            {
                ableToKick = false;
                arrowArt.color = off;
            }
            if(Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
            {
                buildKick = true;
                arrowArt.enabled = true;
            }
            else if(Input.GetButtonUp("Fire1") || Input.GetKeyUp("space"))
            {
                if(ableToKick)
                {
                    KickBall(kickSpeed);
                }
                else
                {
                    kickSpeed = 0f;
                }
                kickTime = 0;
                kickSpeed = 0f;
                buildKick = false;
                arrowArt.enabled = false;
                arrowScaler.localScale = new Vector3(0.25f, 1f, 1f);
            }

            if (!Input.GetButton("Fire1") && !Input.GetKey("space")) {
            arrowArt.enabled = false;
            }
        }
        else
        {
            arrowArt.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if(buildKick)
        {
            kickTime++;
            kickSpeed = Mathf.Log(kickTime * 1.5f) * 50f;
            float scale = Mathf.Log(kickTime) / 2f;
            arrowScaler.localScale = new Vector3(scale, 1f, 1f);
        }
    }

    void KickBall(float speed)
    {
        // Debug.Log("Kicked with speed of " + kickSpeed);
        rb.AddForce(dist * kickSpeed, ForceMode2D.Impulse);
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play("Kick");
        //_audioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Defuse")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            Vector3 pos = col.gameObject.transform.position;
            StartCoroutine(Defuse(pos));
        }
    }

    IEnumerator Defuse(Vector3 pos)
    {
        count = 0;
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
}
