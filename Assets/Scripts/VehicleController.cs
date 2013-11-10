using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    public float ForwardPower = 100;
    public float TurningPower = 25;
    public MovementType Movement = MovementType.Wheels;
    public VehicleType Vehicle = VehicleType.Jeep;
    public bool CanCarryFlag = false;
    public float MovingThreshold = 4f;

    private bool IsMoving = false;
    
	void FixedUpdate () {
        IsMoving = CheckMoving();
	}

    private void MoveForward()
    {
        this.rigidbody.AddForce(this.transform.forward * (this.rigidbody.mass * ForwardPower));
    }

    private void MoveBackward()
    {
        this.rigidbody.AddForce(-this.transform.forward * (this.rigidbody.mass * ForwardPower));
    }

    private void TurnRight()
    {
        if ((IsMoving || this.Movement == MovementType.Treads))
        {
            this.rigidbody.AddTorque(Vector3.up * (this.rigidbody.angularDrag + TurningPower) * this.rigidbody.mass);
        }
    }

    private void TurnLeft()
    {
        if ((IsMoving || this.Movement == MovementType.Treads))
        {
            this.rigidbody.AddTorque(Vector3.down * (this.rigidbody.angularDrag + TurningPower) * this.rigidbody.mass);
        }
    }

    private bool CheckMoving()
    {
        if (this.rigidbody.velocity.sqrMagnitude < MovingThreshold)
        {
            return false;
        }
        return true;
    }
}
