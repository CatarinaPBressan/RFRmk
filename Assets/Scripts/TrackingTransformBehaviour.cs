using UnityEngine;
using System.Collections;

public class TrackingTransformBehaviour : MonoBehaviour
{
    public float trackingRange = 500f;
    public float firingRange = 300f;
    public Transform target;
    
    private bool resetRotation;
    private TurretFiringBehaviour tfb;

    void Start()
    {
        resetRotation = true;
        if (!target)
        {
            return;
        }
        tfb = GetComponent<TurretFiringBehaviour>();
        if (!tfb)
        {
            Debug.Log("This turret has no shooting");  
        }
    }

    void Update()
    {
        var distSqr = Mathf.Pow(this.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(this.transform.position.y - target.transform.position.y, 2);
        if (distSqr <= trackingRange)
        {
            resetRotation = true;
            this.transform.LookAt(target);
            if (distSqr <= firingRange && tfb)
            {
                tfb.SendMessage("Shoot", SendMessageOptions.RequireReceiver);
            }
        }
        else
        {
            if (resetRotation)
            {
                this.transform.rotation = new Quaternion();
                resetRotation = false;
            }
            this.transform.Rotate(Vector3.up, Space.Self);
        }

        if (distSqr <= firingRange)
        {

        }
    }
}
