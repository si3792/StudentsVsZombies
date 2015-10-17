using UnityEngine;
using System.Collections;

public class hide : MonoBehaviour {



	void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
	
	

}
