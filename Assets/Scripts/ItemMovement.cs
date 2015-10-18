using UnityEngine;
using System.Collections;

public class ItemMovement : MonoBehaviour {

    public enum direction
    {
        LEFT, RIGHT
    };
    public direction dir;
    public float speed;
    public float damage = 20;

    void Start()
    {
        if (dir != direction.LEFT)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        
    }

    void Update () {
        if(dir == direction.LEFT)
        {
           // this.transform.Translate(new Vector3(-speed, 0, 0));
        }
        else
        {
            //this.transform.Translate(new Vector3(speed, 0, 0));
        }
        this.transform.Translate(new Vector3(-speed, 0, 0));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            Destroy(this.gameObject);
        }
    }


}
