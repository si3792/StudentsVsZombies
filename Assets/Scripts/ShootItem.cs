using UnityEngine;
using System.Collections;

public class ShootItem : MonoBehaviour {

    public GameObject shootee;

    public void Shoot(ItemMovement.direction dir)
    {
        GameObject go = (GameObject) Instantiate(shootee, this.transform.position, Quaternion.identity);
        go.GetComponent<ItemMovement>().dir = dir;
    }

}
