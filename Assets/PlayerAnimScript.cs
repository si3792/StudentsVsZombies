using UnityEngine;
using System.Collections;

public class PlayerAnimScript : MonoBehaviour {

	void shoot()
    {
        GameObject go = GameObject.FindGameObjectWithTag("shotgunEmitter");

        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        
        go.GetComponent<ShootShotgun>().Shoot(pl.GetComponent<PlayerMovement>().side );
    }

    /*
    // calcs alpha for maximum shine
    public float bonus = 1f;
    float cur = 1;
    bool add = true;
    public float bonusStep = 0.001f;
    float formula(float hp)
    {
        if (hp < 200) return 1f;
        if (hp < 50) return 0.6f;

        if (cur > 1 + bonus && add == true)
        {
            add = false;
        }
        else if (cur < 1 && add == false)
        {
            add = true;
        }
        else if (add == true)
        {
            cur += bonusStep;
        }
        else cur -= bonusStep;

        return cur;
    }*/

    void Update()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        float hp = go.GetComponent<PlayerMovement>().health;
        this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(this.gameObject.GetComponent<SpriteRenderer>().material.color.r, this.gameObject.GetComponent<SpriteRenderer>().material.color.g,
            this.gameObject.GetComponent<SpriteRenderer>().material.color.b, formula(hp)  );
    }
}
