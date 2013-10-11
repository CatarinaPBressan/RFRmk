using UnityEngine;
using System.Collections;

public class TrackingTransformBehaviour : MonoBehaviour
{
    public Team Team;
    
    private bool resetRotation;
    private ShootingBehaviour turretFiringBehaviour;
    private Transform targetTransform;

    void Start()
    {
        resetRotation = true;
        turretFiringBehaviour = GetComponent<ShootingBehaviour>();
    }

    void Update()
    {
        if (targetTransform)
        {
            this.transform.LookAt(targetTransform);
            if (turretFiringBehaviour)
            {
                turretFiringBehaviour.SendMessage("Shoot");
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

    void OnTriggerEnter(Collider enterer)
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

    void OnTriggerExit(Collider exiter)
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
