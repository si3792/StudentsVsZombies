using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour {

   public float speed = 0.02f;
   private enum direction {LEFT, RIGHT, UP, DOWN};
    public float yLimitMax, yLimitMin;
    direction side;
    public float xLimitMax, xLimitMin;


    // Update is called once per frame
    void Update () {

        // flip to left/right
        if (side == direction.RIGHT)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(side == direction.LEFT)
        {
           transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
            
        // movement control
        if(Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x < xLimitMax) { 
                this.transform.Translate(new Vector3(speed, 0, 0));
               }
            side = direction.RIGHT;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.position.x > xLimitMin)
            {
                this.transform.Translate(new Vector3(speed, 0, 0));
            }
            side = direction.LEFT;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (this.transform.position.y > yLimitMin)
            {
                this.transform.Translate(new Vector3(0, -speed, 0));
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (this.transform.position.y < yLimitMax)
            {
                this.transform.Translate(new Vector3(0, speed, 0));
            }
        }


        // force z to 0
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }



}
