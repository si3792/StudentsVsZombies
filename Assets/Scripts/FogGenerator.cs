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
    public float speedRandomization;
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

        int factor = 1;
		float x = Random.Range(minWidthOffset, maxWidthOffset);
		if (Random.value < 0.5) factor = -1;
        x *= factor;
		x += playerControl.transform.position.x;
		
		float y = Random.Range(minY, maxY);
		
		GameObject fogCopy = Instantiate(fogPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
		FogController fogController = fogCopy.GetComponent<FogController>();
		fogController.timeToLive = timeToLive + Random.Range (-randomFactor, randomFactor);
		fogController.opacityFactor = opacityFactor;
		
		fogController.velocity = new Vector2(-1 * factor, 0);
		
		fogController.velocity *= (speed + Random.Range(0, speedRandomization));
        fogCopy.GetComponent<SpriteRenderer>().sprite = spr;
	}
}
