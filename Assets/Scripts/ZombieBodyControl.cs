using UnityEngine;
using System.Collections;

public class ZombieBodyControl : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "item")
        {
            this.gameObject.GetComponentInParent<CreeperController>().health -= other.GetComponent<ItemMovement>().damage;
        }

        if(other.gameObject.tag == "cone")
        {
            this.gameObject.GetComponentInParent<CreeperController>().health -= other.GetComponent<ConeControl>().damage; ;
        }
    }


}
