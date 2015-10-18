using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour {
    public float min = 2f;
    public float max = 4f;
	void Die()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        float a = Random.RandomRange(min, max);
        this.gameObject.transform.localScale *= a;
 
    }

}
