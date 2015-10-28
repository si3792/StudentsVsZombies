using UnityEngine;
using System.Collections;

public class CreeperController : MonoBehaviour {
	
	public double health;
	public Vector3 offset;
	public float speed;
	public float speedRandFactor;
	public NavigationAlgorithms algorithms;
	public float angleRange;
	
	private float objectAvoidanceDist = 1.0f;
	private Rigidbody2D rb2d;
	private Vector2 mov;
	private GameObject target;
    private float effectiveSpeed;
	private enum direction {LEFT, RIGHT, UP, DOWN};
	private direction side;
	private SceneGridManager gridManager;
	private CircleCollider2D collider2d;
	private bool directMove = true;
	private float unstuckTicks = 0;
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SceneGridManager>();
		collider2d = this.gameObject.GetComponent<CircleCollider2D>();
		gridManager.registerObject(this.gameObject, collider2d.transform.position.x, collider2d.transform.position.y, collider2d.radius * 2, collider2d.radius * 2, 5);
		rb2d = GetComponent<Rigidbody2D>();
		algorithms = this.gameObject.GetComponent<NavigationAlgorithms>();
        effectiveSpeed = speed + Random.Range(-speedRandFactor, speedRandFactor);
    }
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
		{
			Destroy(this.gameObject);
		}
		gridManager.updateObjectPosition(this.gameObject);
		
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = 0;
			Vector3 movement = (target.transform.position - posNoZ).normalized * effectiveSpeed;
			
			if (directMove)
			{
				mov = new Vector3(movement.x, movement.y, 0);
			}
			else
			{
				if (unstuckTicks > 0)
				{
					unstuckTicks -= Time.deltaTime;
				}
				else
				{
					float r = Random.value;
					if (r < 0.2) // preserve x randomize y
					{
						mov = new Vector2(movement.x, Random.value - 0.5f).normalized * effectiveSpeed;
					}
					else if (r < 0.3) // Preserve y, randomize x
					{
						mov = new Vector2(Random.value - 0.5f, movement.y).normalized * effectiveSpeed;
					}
					else // Random all the way!
					{
						mov = new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized * effectiveSpeed;
					}
					unstuckTicks = 1;
				}
			}
			
			//Vector2 movementNorm = algorithms.normalizedOptimalMove(gameObject.transform.position.x, gameObject.transform.position.y, target.gameObject.transform.position.x, target.gameObject.transform.position.y);
			//Vector3 movement = new Vector3(movementNorm.x * effectiveSpeed, movementNorm.y * effectiveSpeed, 0);
			//Debug.Log(string.Format("{0} {1}", movementNorm.x, movementNorm.y));
			
			if (movement.x > 0)
			{
				side = direction.RIGHT;
			}
			else if (movement.x < 0)
			{
				side = direction.LEFT;
			}
		}
		else
		{
			mov = new Vector3(0, 0, 0);
		}
		
		if (side == direction.RIGHT)
		{
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		if(side == direction.LEFT)
		{
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
	}
	
	bool CollisionOnTheWay()
	{
		Vector3 posNoZ = transform.position;
		posNoZ.z = 0;
		Vector3 movement = (target.transform.position - posNoZ).normalized * effectiveSpeed;
		Vector3 overlapCheckPos = (movement.normalized * (collider2d.radius + 0.5f) + transform.position);
		RaycastHit2D testHit1 = Physics2D.Raycast(collider2d.transform.position, target.transform.position);
		RaycastHit2D testHit2 = Physics2D.Raycast(collider2d.transform.position + new Vector3(collider2d.radius, 0, 0), target.transform.position);
		RaycastHit2D testHit3 = Physics2D.Raycast(collider2d.transform.position + new Vector3(-collider2d.radius, 0, 0), target.transform.position);
		if (testHit1.collider != null && Vector3.Distance(testHit1.transform.position, this.transform.position) <= collider2d.radius + objectAvoidanceDist)
		{
			Debug.Log("" + testHit1.transform);
			return true;
		}
		if (testHit2.collider != null && Vector3.Distance(testHit2.transform.position, this.transform.position) <= collider2d.radius + objectAvoidanceDist)
		{
			Debug.Log("" + testHit2.transform);
			return true;
		}
		if (testHit3.collider != null && Vector3.Distance(testHit3.transform.position, this.transform.position) <= collider2d.radius + objectAvoidanceDist)
		{
			Debug.Log("" + testHit3.transform);
			return true;
		}
		return false;
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.tag != "Player" && collision.collider.gameObject.tag != "kor") {
			//mov = new Vector3(0, 1, 0) * effectiveSpeed;
			//Debug.Log ("kor");
			directMove = false;
		}
		//Debug.Log("pishka");
	}
	
	void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.gameObject.tag != "Player" && collision.collider.gameObject.tag != "kor") {
			//mov = new Vector3(0, 1, 0) * effectiveSpeed;
			//Debug.Log ("kor");
			directMove = true;
		}
		//Debug.Log("pishka");
	}
	
	void OnDestroy()
	{
		gridManager.removeObject(this.gameObject);
	}
	
	void FixedUpdate()
	{
        if(rb2d != null)
        {
			rb2d.AddForce(mov);
		}
	}
	
}
