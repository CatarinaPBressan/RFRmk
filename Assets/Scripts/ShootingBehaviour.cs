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
    public bool InfiniteShots = false;
    public int MaxAmmoSize = 10;

    private DateTime LastShotTime;
    private bool IsCooledDown = true;
    public int CurrentAmmoCount;

    void Start()
    {
        CurrentAmmoCount = MaxAmmoSize;
    }

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
        if (IsCooledDown && CurrentAmmoCount > 0)
        {
            GameObject bulletInstance = Instantiate(Projectile, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            bulletInstance.transform.Translate(new Vector3(XOffset, YOffset, ZOffset), this.gameObject.transform);
            Collider[] hirearchyColliders = GetParentsColliders();
            foreach (var collider in hirearchyColliders)
            {
                Physics.IgnoreCollision(bulletInstance.GetComponent<Collider>(), collider);
            }
            if (!InfiniteShots)
            {
                CurrentAmmoCount--;
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
            Collider currentCollider = currentTransform.gameObject.GetComponent<Collider>();
            if (currentCollider != null)
            {
                cols.Add(currentCollider);
            }
            currentTransform = currentTransform.transform.parent;
        }
        return cols.ToArray();
    }
}
