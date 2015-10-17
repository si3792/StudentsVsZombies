using UnityEngine;
using System.Collections;

public class SortLayerByY : MonoBehaviour {

    public int sortingOffset = 0;

	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1 + sortingOffset;
        
    }
}
