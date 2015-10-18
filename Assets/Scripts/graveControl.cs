using UnityEngine;
using System.Collections;

public class graveControl : MonoBehaviour {

    public bool empty = true;
    bool dug = false;
    public float dirt = 100;
    public enum loot { EMPTY, WIN}
    public loot graveLoot;
	
	private SceneGridManager gridManager;
	
	void Start()
	{
		gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SceneGridManager>();
		BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
		gridManager.registerObject(this.gameObject, collider.transform.position.x, collider.transform.position.y, collider.size.x, collider.size.y, 5);
	}
	
	void Update () {
	    
        if(dug == false && dirt <= 0)
        {
            dug = true;
            if (empty)
            {
                Destroy(this.gameObject, 1); 
                // ToDo - create used grave
            }
            else
            {
                // ToDo - get artifact / curse / item
            }
        }

	}
	
	void OnDestroy()
	{
		gridManager.removeObject(this.gameObject);
	}
}
