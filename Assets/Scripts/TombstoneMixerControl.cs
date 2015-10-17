using UnityEngine;
using System.Collections;

public class TombstoneMixerControl : MonoBehaviour {

    public int rand; 

    void Start () {
        GameObject[] tombs = GameObject.FindGameObjectsWithTag("graveArea");
        rand = (int)(Random.value * 10000) % tombs.Length;
        tombs[rand].GetComponent<graveControl>().empty = false;
        Debug.Log("Rand tomb:" + rand);

    }
	
	   
    void Update()
    {
        
    }
}
