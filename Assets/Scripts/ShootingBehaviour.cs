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
            Debug.Log(hirearchyColliders);
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
        GameObject currentGO = this.gameObject;
        List<Collider> cols = new List<Collider>();
        while(currentGO != null)
        {
            Debug.Log("Current GO " + currentGO);
            if (currentGO.collider != null)
            {
                Debug.Log("Current GO collider " + currentGO.collider);
                cols.Add(currentGO.collider);
            }
            try
            {
                currentGO = currentGO.transform.parent.gameObject;
            }
            catch (NullReferenceException)
            {
                currentGO = null;
            }
            Debug.Log("Parent GO " + currentGO);
        }
        Debug.Log(cols);
        return cols.ToArray();
    }
}
