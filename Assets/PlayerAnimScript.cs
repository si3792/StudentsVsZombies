using UnityEngine;
using System.Collections;

public class PlayerAnimScript : MonoBehaviour {

	void shoot()
    {
        GameObject go = GameObject.FindGameObjectWithTag("shotgunEmitter");

        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        
        go.GetComponent<ShootShotgun>().Shoot(pl.GetComponent<PlayerMovement>().side );
    }

    public float bloodlust = 0;
    public float bloodlustStep = 1;
    public float bloodlustTimeoutStep = 0.01f;

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


    float formula2()
    {
        if (bloodlust < 0) bloodlust = 0;
        return 1 + bloodlust;
    }
   
    void Update()
    {
        bloodlust -= bloodlustTimeoutStep * Time.deltaTime;

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        float hp = go.GetComponent<PlayerMovement>().health;
        this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(this.gameObject.GetComponent<SpriteRenderer>().material.color.r, this.gameObject.GetComponent<SpriteRenderer>().material.color.g,
            this.gameObject.GetComponent<SpriteRenderer>().material.color.b, formula2()  );
    }
}
