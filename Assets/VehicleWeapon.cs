using UnityEngine;
using System.Collections;

public class VehicleWeapon : MonoBehaviour {


    public Transform projectile;
    public float xOffset;
    public float yOffset;
    public float zOffset;

	// Use this for initialization
	void Start () {
        //Physics.IgnoreCollision(projectile.collider, this.gameObject.collider);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject instance = Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            Debug.Log("instance = " + instance.ToString());
            Debug.Log("instance.collider =" + instance.collider.ToString());


            Physics.IgnoreCollision(instance.collider, this.gameObject.collider);
        }
	}
}
