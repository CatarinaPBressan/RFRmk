using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {

    public float speed = 1;
    public float lifeMs = 1000;


    private DateTime startTime;
	// Use this for initialization
	void Start () {
        startTime = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        DateTime timeNow = DateTime.Now;
        TimeSpan elaspsedTimeSinceStart = timeNow - startTime;
        //Debug.Log(elaspsedTimeSinceLastShot.Milliseconds);

        if (elaspsedTimeSinceStart >= TimeSpan.FromMilliseconds(lifeMs))
        {
        }
	}
}
