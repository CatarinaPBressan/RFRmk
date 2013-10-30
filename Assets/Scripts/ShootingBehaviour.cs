using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShootingBehaviour : MonoBehaviour
{

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

    public void Shoot()
    {
        if (isCooledDown)
        {
            GameObject bulletInstance = Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            bulletInstance.transform.Translate(new Vector3(xOffset, yOffset, zOffset), this.gameObject.transform);
            Collider[] hirearchyColliders = GetParentsColliders();
            foreach (var collider in hirearchyColliders)
            {
                Physics.IgnoreCollision(bulletInstance.collider, collider);
            }
            isCooledDown = false;
            lastShotTime = DateTime.Now;
        }
    }

    private Collider[] GetParentsColliders()
    {
        Transform currentTransform = this.transform;
        List<Collider> cols = new List<Collider>();
        while (currentTransform != null)
        {
            Collider currentCollider = currentTransform.gameObject.collider;
            if (currentCollider != null)
            {
                cols.Add(currentCollider);
            }
            currentTransform = currentTransform.transform.parent;
        }
        return cols.ToArray();
    }
}
