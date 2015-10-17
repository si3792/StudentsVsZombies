using UnityEngine;
using System.Collections;

public class ItemMovement : MonoBehaviour {

    public enum direction
    {
        LEFT, RIGHT
    };
    public direction dir;
    public float speed;
    
    void Update () {
        if(dir == direction.LEFT)
        {
            this.transform.Translate(new Vector3(-speed, 0, 0));
        }
        else
        {
            this.transform.Translate(new Vector3(speed, 0, 0));
        }
    }
	
	
}
