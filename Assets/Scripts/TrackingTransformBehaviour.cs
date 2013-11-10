using UnityEngine;
using System.Collections;

public class TrackingTransformBehaviour : MonoBehaviour
{
    public Team Team;

    private bool ResetRotation = true;
    private bool IsTargetInRange = false;
    private ShootingBehaviour TurretShooting;
    private Transform Target;

    void Start()
    {
        TurretShooting = GetComponent<ShootingBehaviour>();
    }

    void Update()
    {
        if (Target)
        {
            this.transform.LookAt(Target);
            if (TurretShooting && IsTargetInRange)
            {
                TurretShooting.Shoot();
            }
        }
        else
        {
            this.transform.Rotate(Vector3.up, Space.Self);
        }

        if (ResetRotation)
        {
            this.transform.rotation = new Quaternion();
            ResetRotation = false;
        }        
    }

    internal void StartTracking(Collider enterer)
    {
        var playerController = enterer.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            if (!playerController.Team.Equals(this.Team))
            {
                Target = enterer.gameObject.transform;
            }
        }
    }

    internal void StartShooting()
    {
        IsTargetInRange = true;
    }

    internal void StopShooting()
    {
        IsTargetInRange = false;
    }

    internal void StopTracking(Collider exiter)
    {
        var playerController = exiter.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            if (playerController.gameObject.transform.Equals(Target))
            {
                Target = null;
                ResetRotation = true;
            }
        }
    }
}
