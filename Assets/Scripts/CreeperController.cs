using UnityEngine;
using System.Collections;

public class CreeperController : MonoBehaviour {
	
	public double health;
	public Vector3 offset;
	public float speed;
	public float speedRandFactor;
	
	private Rigidbody2D rb2d;
	private Vector2 mov;
	private GameObject target;
    private float effectiveSpeed;
	private enum direction {LEFT, RIGHT, UP, DOWN};
	private direction side;
	private SceneGridManager gridManager;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SceneGridManager>();
		CircleCollider2D collider = this.gameObject.GetComponent<CircleCollider2D>();
		gridManager.registerObject(this.gameObject, collider.transform.position.x, collider.transform.position.y, collider.radius * 2, collider.radius * 2, 5);
		rb2d = GetComponent<Rigidbody2D>();
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
			posNoZ.z = target.transform.position.z;	
			Vector3 movement = (target.transform.position - posNoZ).normalized * effectiveSpeed;
			mov = new Vector3(movement.x, movement.y);
			
			if (movement.x > 0)
			{
				side = direction.RIGHT;
			}
			else
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
	
	void OnDestroy()
	{
		gridManager.removeObject(this.gameObject);
	}
	
	void FixedUpdate()
	{
        if(rb2d != null)
		rb2d.AddForce(mov);
	}
	
}
