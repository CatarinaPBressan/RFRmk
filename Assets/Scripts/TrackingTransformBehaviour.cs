using UnityEngine;
using System.Collections;

public class TrackingTransformBehaviour : MonoBehaviour
{
    public Team Team;

    private bool resetRotation = true;
    private bool targetInRange = false;
    private ShootingBehaviour turretFiringBehaviour;
    private Transform targetTransform;

    void Start()
    {
        turretFiringBehaviour = GetComponent<ShootingBehaviour>();
    }

    void Update()
    {
        if (targetTransform)
        {
            this.transform.LookAt(targetTransform);
            if (turretFiringBehaviour && targetInRange)
            {
                turretFiringBehaviour.Shoot();
            }
        }
        else
        {
            this.transform.Rotate(Vector3.up, Space.Self);
        }

        if (resetRotation)
        {
            this.transform.rotation = new Quaternion();
            resetRotation = false;
        }        
    }

    internal void StartTracking(Collider enterer)
    {
        var playerController = enterer.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            if (!playerController.Team.Equals(this.Team))
            {
                targetTransform = enterer.gameObject.transform;
            }
        }
    }

    internal void StartShooting()
    {
        targetInRange = true;
    }

    internal void StopShooting()
    {
        targetInRange = false;
    }

    internal void StopTracking(Collider exiter)
    {
        var playerController = exiter.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            if (playerController.gameObject.transform.Equals(targetTransform))
            {
                targetTransform = null;
                resetRotation = true;
            }
        }
    }
}
