using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class killcountScript : MonoBehaviour {

	
	void Update () {
        this.GetComponent<Text>().text =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().killcount.ToString();
	}
}
