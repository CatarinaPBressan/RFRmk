using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    public float forwardPower = 100;
    public float turningPower = 25;
    public MovementType movementType = MovementType.Wheels;
    public bool canCarryFlag = false;
    public float movingThreshold = 4f;

    private bool _isMoving = false;
    private SmoothFollow _sf;
    
	void Start () {
        _sf = Camera.mainCamera.GetComponent<SmoothFollow>() as SmoothFollow;
	}

    void Update()
    {
    }
	
	void FixedUpdate () {
        _isMoving = CheckMoving();
	}

    private void MoveForward()
    {
        this.rigidbody.AddForce(this.transform.forward * (this.rigidbody.mass * forwardPower));
    }

    private void MoveBackward()
    {
        this.rigidbody.AddForce(-this.transform.forward * (this.rigidbody.mass * forwardPower));
    }

    private void TurnRight()
    {
        if ((_isMoving || this.movementType == MovementType.Treads))
        {
            this.rigidbody.AddTorque(Vector3.up * (this.rigidbody.angularDrag + turningPower) * this.rigidbody.mass);
        }
    }

    private void TurnLeft()
    {
        if ((_isMoving || this.movementType == MovementType.Treads))
        {
            this.rigidbody.AddTorque(Vector3.down * (this.rigidbody.angularDrag + turningPower) * this.rigidbody.mass);
        }
    }

    private bool CheckMoving()
    {
        if (this.rigidbody.velocity.sqrMagnitude < movingThreshold)
        {
            return false;
        }
        return true;
    }
}
