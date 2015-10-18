using UnityEngine;
using System.Collections;

public class FogGenerator : MonoBehaviour {
	
	public float timeBetweenSpawns;
	public float timeToLive;
	public float opacityFactor;
	public float speed;
	public float randomFactor;
	public float minX, minY, maxX, maxY;
	public float minWidthOffset, maxWidthOffset;
	public int count;
	
	public Sprite s1;
	public Sprite s2;
	public Sprite s3;
	public Sprite s4;
	public Sprite s5;
	
	public GameObject fogPrefab;
	private float instTime;
	private PlayerMovement playerControl;
	// Use this for initialization
	void Start () {
		instTime = timeBetweenSpawns + Random.Range(-randomFactor, randomFactor);
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		instTime -= Time.deltaTime;
		if (instTime <= 0)
		{
			instTime = timeBetweenSpawns + Random.Range(-randomFactor, randomFactor);
			for (int i = 0; i< count; i++)
				SpawnFog();
		}
	}
	
	void SpawnFog() {
		var r = Random.Range(0, 1);
		Sprite spr;
		if (r < 0.2) spr = s1;
		else if (r < 0.4) spr = s2;
		else if (r < 0.6) spr = s3;
		else if (r < 0.8) spr = s4;
		else spr = s5;
		
		float x = Random.Range(minWidthOffset, maxWidthOffset);
		if (Random.Range(0, 1) < 0.5) x = -x;
		x += playerControl.transform.position.x;
		
		float y = Random.Range(minY, maxY);
		
		GameObject fogCopy = Instantiate(fogPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
		FogController fogController = fogCopy.GetComponent<FogController>();
		fogController.timeToLive = timeToLive + Random.Range (-randomFactor, randomFactor);
		fogController.opacityFactor = opacityFactor;
		
		if (Random.Range (0, 1) < 0.5) fogController.velocity = new Vector2(1, 0);
		else fogController.velocity = new Vector2(-1, 0);
		
		fogController.velocity *= speed;
		fogCopy.GetComponent<SpriteRenderer>().sprite = spr;
	}
}
