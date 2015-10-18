using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {

    public float timer;
	
	void Update () {

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

	}

    void OnApplicationQuit()
    {
        Destroy(this.gameObject);
    }
}
