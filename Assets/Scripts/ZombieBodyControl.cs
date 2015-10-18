using UnityEngine;
using System.Collections;

public class ZombieBodyControl : MonoBehaviour {

    public GameObject spamee;
    public GameObject spamee2;
    public GameObject spameeME;

    void OnDestroy()
    {
        Instantiate(spameeME, this.gameObject.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "item")
        {

            Instantiate(spamee2, this.gameObject.transform.position, Random.rotation);
            Instantiate(spamee2, this.gameObject.transform.position, Random.rotation);
            this.gameObject.GetComponentInParent<CreeperController>().health -= other.GetComponent<ItemMovement>().damage;
        }

        if(other.gameObject.tag == "cone")
        {
            Instantiate(spamee, this.gameObject.transform.position, Random.rotation);
            Instantiate(spamee, this.gameObject.transform.position, Random.rotation);
            Instantiate(spamee, this.gameObject.transform.position, Random.rotation);
            Instantiate(spamee, this.gameObject.transform.position, Random.rotation);
            this.gameObject.GetComponentInParent<CreeperController>().health -= other.GetComponent<ConeControl>().damage;
            GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<PlayerAnimScript>().bloodlust += GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<PlayerAnimScript>().bloodlustStep;
        }
    }


}
