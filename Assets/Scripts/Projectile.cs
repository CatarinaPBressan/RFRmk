using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {

    public float Speed = 1;
    public float LifeMs = 1000;
    public int Damage = 10;

	void Start () {
        Destroy(this.gameObject, LifeMs / 1000);
	}
	
	void Update () {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        HealthCounter hc = collisionInfo.gameObject.GetComponentInChildren<HealthCounter>() as HealthCounter;
        if (hc != null)
        {
            hc.Damage(this.Damage);
        }
        Destroy(this.gameObject);
    }

}
