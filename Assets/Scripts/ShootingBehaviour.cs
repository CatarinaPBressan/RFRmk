using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShootingBehaviour : MonoBehaviour
{

    public GameObject Projectile;
    public float XOffset;
    public float YOffset;
    public float ZOffset;
    public float CooldownTimeMs;

    private DateTime LastShotTime;
    private bool IsCooledDown;

    // Use this for initialization
    void Start()
    {
        IsCooledDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCooledDown)
        {
            DateTime timeNow = DateTime.Now;
            TimeSpan elaspsedTimeSinceLastShot = timeNow - LastShotTime;
            if (elaspsedTimeSinceLastShot >= TimeSpan.FromMilliseconds(CooldownTimeMs))
            {
                IsCooledDown = true;
            }
        }
    }

    public void Shoot()
    {
        if (IsCooledDown)
        {
            GameObject bulletInstance = Instantiate(Projectile, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            bulletInstance.transform.Translate(new Vector3(XOffset, YOffset, ZOffset), this.gameObject.transform);
            Collider[] hirearchyColliders = GetParentsColliders();
            foreach (var collider in hirearchyColliders)
            {
                Physics.IgnoreCollision(bulletInstance.collider, collider);
            }
            IsCooledDown = false;
            LastShotTime = DateTime.Now;
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
