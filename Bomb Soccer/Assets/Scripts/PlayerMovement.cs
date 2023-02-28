using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

        public Rigidbody2D rb;
        public SpriteRenderer sr;
        public float moveSpeed = 5f;
        public float halfSpeed = 3.535f;
        public float fullSpeed = 5f;
        public Vector2 movement;
        public Animator anim;

        private bool sliding = false;
        private bool startSlide = false;
        public bool canMove = false;

        // https://answers.unity.com/questions/52017/2-audio-sources-on-a-game-object-how-use-script-to.html
        public AudioSource step1Audio;
        public AudioSource step2Audio;
        public AudioSource step3Audio;
        public AudioSource step4Audio;
        private int nextUpdate = 1;

        // Auto-load the RigidBody component into the variable:
        void Start(){
            rb = GetComponent<Rigidbody2D> ();
            sr = transform.Find("Player Art").gameObject.GetComponent<SpriteRenderer>();
            canMove = false;
        }

        public void StartLevel()
        {
            canMove = true;
        }

        public void GameOver()
        {
            canMove = false;
            anim.enabled = false;
        }

        public void WinLevel()
        {
            canMove = false;
            anim.enabled = false;
        }

        void Update()
        {
            if(canMove)
            {
                movement.x = Input.GetAxisRaw ("Horizontal");
                movement.y = Input.GetAxisRaw ("Vertical");
                if(movement.x != 0 && movement.y != 0)
                {
                    moveSpeed = halfSpeed;
                    anim.enabled = true;
                }
                else if(movement.x == 0 && movement.y == 0)
                {
                    anim.enabled = false;
                }
                else
                {
                    anim.enabled = true;
                    moveSpeed = fullSpeed;
                }
                MoveSoundUpdate();

                if(movement.x > 0)
                {
                    anim.Play("RightWalk");
                    sr.flipX = false;
                }
                if(movement.x < 0)
                {
                    anim.Play("RightWalk");
                    sr.flipX = true;
                }

                if(Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
                {
                    sliding = true;
                    startSlide = true;
                }
                if(Input.GetButtonUp("Fire1") || Input.GetKeyUp("space"))
                {
                    sliding = false;
                    startSlide = false;
                    rb.velocity = Vector3.zero;
                    //_audioSource.Play();
                }
            }
            else
            {
                anim.enabled = false;
            }
        }

        void MoveSoundUpdate(){
          // Code for footstep sounds
          // If the next update is reached
          if(Time.time >= nextUpdate){
             // Change the next update (current second+1)
             nextUpdate=Mathf.FloorToInt(Time.time)+1;
             // Call your fonction
             UpdateEverySecond();
          }
        }

        // Update is called once per second
       void UpdateEverySecond(){
            // if (nextUpdate % 4 == 0) {
            //     //Debug.Log($"AudioSource is {step1Audio.enabled ? "enabled" : "disabled!"}", this);
            //     step1Audio.Play();
            // } else if (nextUpdate % 4 == 1) {
            //     step2Audio.Play();
            // } else if (nextUpdate % 4 == 2) {
            //     step3Audio.Play();
            // } else if (nextUpdate % 4 == 3) {
            //     step4Audio.Play();
            // }
       }

        // Listen for player input to move the object:
        void FixedUpdate()
        {
            if(canMove)
            {
                if(sliding)
                {
                    if(startSlide)
                    {
                        rb.AddForce(movement * moveSpeed * 1000 * Time.fixedDeltaTime);
                        startSlide = false;
                    }
                }
                else
                {
                    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                }
            }
        }

        // Makes objects with the tag "tree" disappear on contact:
        void OnCollisionEnter2D(Collision2D other){
                /* if (other.gameObject.tag == "tree"){
                    Destroy(other.gameObject);
                } */
        }
}
