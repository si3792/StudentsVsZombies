using UnityEngine;
using System.Collections;

public class PlayerAnimScript : MonoBehaviour {

	void shoot()
    {
        GameObject go = GameObject.FindGameObjectWithTag("shotgunEmitter");

        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        
        go.GetComponent<ShootShotgun>().Shoot(pl.GetComponent<PlayerMovement>().side );
    }
}
