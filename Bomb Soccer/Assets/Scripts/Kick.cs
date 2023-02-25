using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kick : MonoBehaviour
{
    private Transform player;
    private Collider2D col;
    private Collider2D kickZone;
    private GameObject arrow;
    private SpriteRenderer arrowArt;
    private Rigidbody2D rb;

    public float kickRadius = 3f;

    bool ableToKick = false;
    bool buildKick = false;
    public float kickSpeed = 0f;
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
        arrow = transform.Find("Arrow").gameObject;
        arrowArt = arrow.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        arrowArt.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
        if(Input.GetButtonUp("Fire1") || Input.GetKeyUp("space"))
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
        }
    }

    void FixedUpdate()
    {
        if(buildKick)
        {
            kickTime++;
            kickSpeed = Mathf.Log(kickTime) * 50f;
        }
    }

    void KickBall(float speed)
    {
        // Debug.Log("Kicked with speed of " + kickSpeed);
        rb.AddForce(dist * kickSpeed, ForceMode2D.Impulse);
    }
}
