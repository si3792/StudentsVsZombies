using UnityEngine;
using System.Collections;


public class Restart : MonoBehaviour {

    public void restart()
    {
        //Debug.Log("RESTART CALLED");
        Application.LoadLevel(Application.loadedLevel);
    }

    public void exitt()
    {
        //Debug.Log("RESTART CALLED");
        Application.LoadLevel("mapscene");
    }

    void Update () {
	    
	}

    

}
