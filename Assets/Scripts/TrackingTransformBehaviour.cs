using UnityEngine;
using System.Collections;

public class TrackingTransformBehaviour : MonoBehaviour
{
    public float trackingRange = 500f;
    public float firingRange = 300f;
    public Transform targetTransform;
    
    private bool resetRotation;
    private ShootingBehaviour tfb;

    void Start()
    {
        resetRotation = true;
        if (!targetTransform)
        {
            return;
        }
        tfb = GetComponent<ShootingBehaviour>();
    }

    void Update()
    {
        if (targetTransform)
        {
            var distSqr = Mathf.Pow(this.transform.position.x - targetTransform.transform.position.x, 2) + Mathf.Pow(this.transform.position.y - targetTransform.transform.position.y, 2);
            if (distSqr <= trackingRange)
            {
                resetRotation = true;
                this.transform.LookAt(targetTransform);
                if (distSqr <= firingRange && tfb)
                {
                    tfb.SendMessage("Shoot");
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
        }
        else
        {

        }
    }
}
