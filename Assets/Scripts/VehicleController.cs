using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {

    public float ForwardPower = 100;
    public float TurningPower = 25;
    public MovementType Movement = MovementType.Wheels;
    public VehicleType Vehicle = VehicleType.Jeep;
    public bool CanCarryFlag = false;
    public float MovingThreshold = 4f;
    public int MaxFuelUnits = 100;
    public bool ConsumeFuelOnTurn = false;

    private int CurrentFuelUnits;
    private bool IsMoving;
    private static readonly int FUEL_CONSUMPTION_RATE = 1;


    void Start()
    {
        CurrentFuelUnits = MaxFuelUnits;
    }

    void FixedUpdate()
    {
        IsMoving = CheckMoving();
    }

    public void MoveForward()
    {
        if (CurrentFuelUnits > 0)
        {
            this.rigidbody.AddForce(this.transform.forward * (this.rigidbody.mass * ForwardPower));
            CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
        }
    }

    public void MoveBackward()
    {
        if (CurrentFuelUnits > 0)
        {
            this.rigidbody.AddForce(-this.transform.forward * (this.rigidbody.mass * ForwardPower));
            CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
        }
    }

    public void TurnRight()
    {
        if ((IsMoving || this.Movement == MovementType.Treads) && (CurrentFuelUnits > 0 || !ConsumeFuelOnTurn))
        {
            this.rigidbody.AddTorque(Vector3.up * (this.rigidbody.angularDrag + TurningPower) * this.rigidbody.mass);
            if (ConsumeFuelOnTurn)
            {
                CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
            }
        }
    }

    public void TurnLeft()
    {
        if ((IsMoving || this.Movement == MovementType.Treads) && (CurrentFuelUnits > 0 || !ConsumeFuelOnTurn))
        {
            this.rigidbody.AddTorque(Vector3.down * (this.rigidbody.angularDrag + TurningPower) * this.rigidbody.mass);
            if (ConsumeFuelOnTurn)
            {
                CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
            }
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
