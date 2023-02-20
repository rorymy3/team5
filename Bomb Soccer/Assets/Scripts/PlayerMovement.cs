using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

        public Rigidbody2D rb;
        public float moveSpeed = 5f;
        public float halfSpeed = 3.535f;
        public float fullSpeed = 5f;
        public Vector2 movement;

        private bool sliding = false;
        private bool startSlide = false;

        // Auto-load the RigidBody component into the variable:
        void Start(){
            rb = GetComponent<Rigidbody2D> ();
        }

        void Update() 
        {
            if ((Input.GetKey("w") || Input.GetKey("up")) && (Input.GetKey("d") || Input.GetKey("right")))
            {
                moveSpeed = halfSpeed;
            }
            else if ((Input.GetKey("w") || Input.GetKey("up")) && (Input.GetKey("a") || Input.GetKey("left")))
            {
                moveSpeed = halfSpeed;
            }
            else if ((Input.GetKey("s") || Input.GetKey("down")) && (Input.GetKey("d") || Input.GetKey("right")))
            {
                moveSpeed = halfSpeed;
            }
            else if ((Input.GetKey("s") || Input.GetKey("down")) && (Input.GetKey("a") || Input.GetKey("left")))
            {
                moveSpeed = halfSpeed;
            }
            else
            {
                moveSpeed = fullSpeed;
            }
            movement.x = Input.GetAxisRaw ("Horizontal");
            movement.y = Input.GetAxisRaw ("Vertical");
            
            if(Input.GetButtonDown("Fire1"))
            {
                sliding = true;
                startSlide = true;
            }
            if(Input.GetButtonUp("Fire1"))
            {
                sliding = false;
                startSlide = false;
                rb.velocity = Vector3.zero;
                // rb.angularVelocity = Vector3.zero; 
            }
        }

        // Listen for player input to move the object:
        void FixedUpdate()
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

        // Makes objects with the tag "tree" disappear on contact:
        void OnCollisionEnter2D(Collision2D other){
                /* if (other.gameObject.tag == "tree"){
                    Destroy(other.gameObject);
                } */
        }
}
