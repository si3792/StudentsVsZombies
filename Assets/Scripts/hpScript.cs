using UnityEngine;
using UnityEngine.UI;

public class hpScript : MonoBehaviour {

	
	void Update () {

        this.gameObject.GetComponent<Image>().fillAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().health / 400;

	}
}
