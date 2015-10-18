using UnityEngine;
using System.Collections;

public class PlayerAnimScript : MonoBehaviour {

	void shoot()
    {
        GameObject go = GameObject.FindGameObjectWithTag("shotgunEmitter");

        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        
        go.GetComponent<ShootShotgun>().Shoot(pl.GetComponent<PlayerMovement>().side );
    }


    void Update()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        float hp = go.GetComponent<PlayerMovement>().health;
        this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(this.gameObject.GetComponent<SpriteRenderer>().material.color.r, this.gameObject.GetComponent<SpriteRenderer>().material.color.g,
            this.gameObject.GetComponent<SpriteRenderer>().material.color.b, hp / 400);
    }
}
