using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kick : MonoBehaviour
{
    private Transform player;
    private Collider2D playerCol;
    private Collider2D kickZone;
    private GameObject arrow;
    private SpriteRenderer arrowArt;
    private Rigidbody2D rb;

    public float kickRadius = 3f;

    bool ableToKick = false;
    bool buildKick = false;
    float kickSpeed = 0f;
    int kickTime = 0;

    public Color off;
    public Color on;

    // distance between player and object.
    public Vector3 dist;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCol = GameObject.Find("Player").GetComponent<Collider2D>();
        player = GameObject.Find("Player").transform;
        kickZone = transform.Find("Kick Zone").gameObject.GetComponent<Collider2D>();
        arrow = transform.Find("Arrow").gameObject;
        arrowArt = arrow.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        arrowArt.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = transform.position - player.position;

        // float angle = Vector3.Angle(transform.position, player.position);
        // Debug.Log(angle);

        arrow.transform.right = -(player.position - transform.position);

        if(playerCol.IsTouching(kickZone))
        {
            ableToKick = true;
            arrowArt.color = on;
        }
        else
        {
            ableToKick = false;
            arrowArt.color = off;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            buildKick = true;
            arrowArt.enabled = true;
        }
        if(Input.GetButtonUp("Fire1"))
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
            kickSpeed = Mathf.Log(kickTime) * 5f;
        }
    }

    void KickBall(float speed)
    {
        Debug.Log("Kicked with speed of " + kickSpeed);
        rb.AddForce(dist * kickSpeed, ForceMode2D.Impulse);
    }
}
