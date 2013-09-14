using UnityEngine;
using System.Collections;
using System;

public class TurretFiringBehaviour : MonoBehaviour {

    public GameObject projectile;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float cooldownTimeMs;

    private DateTime lastShotTime;
    private bool isCooledDown;

    // Use this for initialization
    void Start()
    {
        isCooledDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCooledDown)
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan elaspsedTimeSinceLastShot = timeNow - lastShotTime;
            if (elaspsedTimeSinceLastShot >= TimeSpan.FromMilliseconds(cooldownTimeMs))
            {
                isCooledDown = true;
            }
        }
    }

    void Shoot()
    {
        if (isCooledDown)
        {
            //Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
            GameObject instance = Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            instance.transform.Translate(new Vector3(xOffset, yOffset, zOffset), this.gameObject.transform);
            Physics.IgnoreCollision(instance.collider, this.gameObject.collider);
            isCooledDown = false;
            lastShotTime = DateTime.Now;
        }
    }
}
