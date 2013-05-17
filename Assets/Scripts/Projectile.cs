using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {

    public float Speed = 1;
    public float LifeMs = 1000;
    public int Damage = 10;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, LifeMs / 1000); //schedule the object for deletion
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        HealthCounter hc = collisionInfo.gameObject.GetComponent<HealthCounter>();

        if (hc != null)
        {
            hc.Damage(this.Damage);
            //Debug.Log("damaged!");
        }

        Destroy(this.gameObject);
    }

}
