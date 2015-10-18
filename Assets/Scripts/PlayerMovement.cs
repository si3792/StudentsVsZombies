﻿using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour {

    public float health = 200;
    public bool dead = false;
   public float speed = 0.02f;
    public float diggingSpeed = 0.2f;
  // public enum ItemMovement.direction {LEFT, RIGHT, UP, DOWN};
    public float yLimitMax, yLimitMin;
    public ItemMovement.direction side;
    public float xLimitMax, xLimitMin;
    private Rigidbody2D rb2d;
    private Vector2 mov;


    void Control()
    {
        if (dead == true) return;


        // movement control
        mov = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (this.transform.position.x < xLimitMax)
            {
                //this.transform.Translate(new Vector3(speed, 0, 0));
                mov = new Vector2(mov.x + speed, mov.y);
            }
            side = ItemMovement.direction.RIGHT;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (this.transform.position.x > xLimitMin)
            {
                //this.transform.Translate(new Vector3(speed, 0, 0));
                mov = new Vector2(mov.x - speed, mov.y);
            }
            side = ItemMovement.direction.LEFT;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (this.transform.position.y > yLimitMin)
            {
                //this.transform.Translate(new Vector3(0, -speed, 0));
                mov = new Vector2(mov.x, mov.y - speed);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (this.transform.position.y < yLimitMax)
            {
                //this.transform.Translate(new Vector3(0, speed, 0));
                mov = new Vector2(mov.x, mov.y + speed);
            }
        }


        // shooting
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (side == ItemMovement.direction.LEFT)
                GameObject.FindGameObjectWithTag("itemEmitter").GetComponent<ShootItem>().Shoot(ItemMovement.direction.LEFT);

            if (side == ItemMovement.direction.RIGHT)
                GameObject.FindGameObjectWithTag("itemEmitter").GetComponent<ShootItem>().Shoot(ItemMovement.direction.RIGHT);
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<Animator>().SetTrigger("Attack");
        }


    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.AddForce(mov);
    }

    // Update is called once per frame
    void Update () {

        // flip to left/right
        if (side == ItemMovement.direction.RIGHT)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(side == ItemMovement.direction.LEFT)
        {
           transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        Control();

        //die to low hp
        if (health <= 0) dead = true;

        // force z to 0
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "graveArea")
        {
            if(Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<graveControl>().dirt -= diggingSpeed;
            }
        }

        if (other.gameObject.tag == "Zombie")
        {
            health -= 1;
            GameObject cam = GameObject.FindGameObjectWithTag("Shaker");
            cam.GetComponent<PerlinShake>().test = true;
        }
    }

}
