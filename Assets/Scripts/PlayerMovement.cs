﻿using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour {

    public AudioClip sword;
    public AudioClip death;
    public float health = 200;
    public int killcount = 0;
    public bool dead = false;
    public float maxSpeed;
   public float speed = 0.02f;
    public float diggingSpeed = 0.2f;
  // public enum ItemMovement.direction {LEFT, RIGHT, UP, DOWN};
    public float yLimitMax, yLimitMin;
    public ItemMovement.direction side;
    public float xLimitMax, xLimitMin;
    private Rigidbody2D rb2d;
    private Vector2 mov;
	private SceneGridManager gridManager;
	public GameObject gameOver;
    void Control()
    {
        if (dead == true) return;


        // movement control
        mov = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
        {
            if (this.transform.position.x < xLimitMax)
            {
                //this.transform.Translate(new Vector3(speed, 0, 0));
                mov = new Vector2(mov.x + speed, mov.y);
            }
            side = ItemMovement.direction.RIGHT;
        }
		else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
        {
            if (this.transform.position.x > xLimitMin)
            {
                //this.transform.Translate(new Vector3(speed, 0, 0));
                mov = new Vector2(mov.x - speed, mov.y);
            }
            side = ItemMovement.direction.LEFT;
        }

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0)
        {
            if (this.transform.position.y > yLimitMin)
            {
                //this.transform.Translate(new Vector3(0, -speed, 0));
                mov = new Vector2(mov.x, mov.y - speed);
            }
        }
		else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            if (this.transform.position.y < yLimitMax)
            {
                //this.transform.Translate(new Vector3(0, speed, 0));
                mov = new Vector2(mov.x, mov.y + speed);
            }
        }


        // shooting
        var triggerTreshold = 0.999;
        if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Fire1") > triggerTreshold)
        {
            GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<Animator>().SetTrigger("Shoot");
        }


		if(Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Fire2") > triggerTreshold)
        {
            
            GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<Animator>().SetTrigger("Attack");
        }

  
    }

    public void playWhoosh()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(sword);
    }


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SceneGridManager>();
		BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
		gridManager.registerPlayer(this.gameObject, collider.transform.position.x, collider.transform.position.y, collider.size.x, collider.size.y, -100);
    }

    void FixedUpdate()
    {
        rb2d.AddForce(mov);
    }

    // Update is called once per frame
    void Update () {

        if(speed < maxSpeed)
        {
            speed += 1;
        }

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
        if (health <= 0 && !dead)
        {
        	dead = true;
            this.gameObject.GetComponent<AudioSource>().volume = 1f;
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(death);
            //Instantiate(gameOver, new Vector3(transform.position.x, -8, 0), Quaternion.identity);
        }
		// Update grid manager
		gridManager.updateObjectPosition(this.gameObject);
		
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
            if (speed > 20) speed -= 5;
            GameObject cam = GameObject.FindGameObjectWithTag("Shaker");
            cam.GetComponent<PerlinShake>().test = true;
        }
    }
	
	void OnDestroy()
	{
		gridManager.removeObject(this.gameObject);
	}
}
