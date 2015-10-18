using UnityEngine;
using System.Collections;

public class NavigationAlgorithms : MonoBehaviour {

	private SceneGridManager gridManager;
	// Use this for initialization
	void Start () {
		gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SceneGridManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
