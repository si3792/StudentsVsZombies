using UnityEngine;
using System.Collections;

public class playButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        this.gameObject.GetComponent<SpriteRenderer>().enabled = GameObject.FindGameObjectWithTag("tagMag").GetComponent<LevelChangerController>().selected;

	}
}
