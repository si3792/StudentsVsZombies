using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class visOnDeath : MonoBehaviour {


	void Update () {
        this.GetComponent<Image>().enabled = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().dead;
	}
}
