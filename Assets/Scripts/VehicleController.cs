using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleController : VehicleBase {

    public float ForwardPower = 100;
    public float TurningPower = 25;
    public MovementType Movement = MovementType.Wheels;
    public VehicleType Vehicle = VehicleType.Jeep;
    public bool CanCarryFlag = false;
    public float MovingThreshold = 4f;
    public bool ConsumeFuelOnTurn = false;
    private bool IsMoving;
    private static readonly float FUEL_CONSUMPTION_RATE = 0.1f;


    void FixedUpdate()
    {
        base.FixedUpdate();

        IsMoving = CheckMoving();
    }

    public void MoveForward()
    {
        if (CurrentFuelUnits > 0)
        {
            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * (this.GetComponent<Rigidbody>().mass * ForwardPower));
           CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
        }
    }

  
    public void MoveBackward()
    {
        if (CurrentFuelUnits > 0)
        {
            this.GetComponent<Rigidbody>().AddForce(-this.transform.forward * (this.GetComponent<Rigidbody>().mass * ForwardPower));
            CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
        }
    }

    public void TurnRight()
    {
        if ((IsMoving || this.Movement == MovementType.Treads) && (CurrentFuelUnits > 0 || !ConsumeFuelOnTurn))
        {
            this.GetComponent<Rigidbody>().AddTorque(Vector3.up * (this.GetComponent<Rigidbody>().angularDrag + TurningPower) * this.GetComponent<Rigidbody>().mass);
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
            this.GetComponent<Rigidbody>().AddTorque(Vector3.down * (this.GetComponent<Rigidbody>().angularDrag + TurningPower) * this.GetComponent<Rigidbody>().mass);
            if (ConsumeFuelOnTurn)
            {
                CurrentFuelUnits -= FUEL_CONSUMPTION_RATE;
            }
        }
    }

    private bool CheckMoving()
    {
        if (this.GetComponent<Rigidbody>().velocity.sqrMagnitude < MovingThreshold)
        {
            return false;
        }
        return true;
    }
}
