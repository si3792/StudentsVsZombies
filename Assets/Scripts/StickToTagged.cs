using UnityEngine;
using System.Collections;

public class StickToTagged : MonoBehaviour {

    public string tag;

	void Update () {
        this.transform.position = GameObject.FindGameObjectWithTag(tag).transform.position;
	}
}
