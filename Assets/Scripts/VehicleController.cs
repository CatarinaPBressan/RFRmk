using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleController : MonoBehaviour {

    public float ForwardPower = 100;
    public float TurningPower = 25;
    public MovementType Movement = MovementType.Wheels;
    public VehicleType Vehicle = VehicleType.Jeep;
    public bool CanCarryFlag = false;
    public float MovingThreshold = 4f;
    public int MaxFuelUnits = 100;
    public bool ConsumeFuelOnTurn = false;
    private float CurrentFuelUnits;
    private Scrollbar FuelIndicator;
    private bool IsMoving;
    private static readonly int FUEL_CONSUMPTION_RATE = 1;


    void Start()
    {
        CurrentFuelUnits = MaxFuelUnits;

        FuelIndicator = GetComponentInChildren<Scrollbar>();
    }

    void FixedUpdate()
    {
        Debug.Log("valor do fuel = "  + (CurrentFuelUnits / 100));
        Debug.Log("valor do size indicador = " + FuelIndicator.size);
        FuelIndicator.size =  CurrentFuelUnits / 100f;

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
