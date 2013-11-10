using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Team Team;

    public bool CanCarryFlag
    {
        get
        {
            if (Vehicle != null)
            {
                return Vehicle.CanCarryFlag;
            }
            return false;
        }
    }

    private VehicleController Vehicle;
    private ShootingBehaviour ShootingBehaviour;
    private MatchManager Manager;
    private TankTurretBehaviour TankTurret;

    private bool CanControlBase = false;
    private bool ChangingVehicle = false;

	void Start () 
    {
        Vehicle = gameObject.GetComponent<VehicleController>() as VehicleController;
        ShootingBehaviour = gameObject.GetComponentInChildren<ShootingBehaviour>() as ShootingBehaviour;
        TankTurret = gameObject.GetComponentInChildren<TankTurretBehaviour>() as TankTurretBehaviour;
        Manager = Object.FindObjectOfType(typeof(MatchManager)) as MatchManager;
	}
	
    void Update()
    {
        //TODO: Replace SendMessages
        if (!Manager.GameEnded)
        {
            if (!ChangingVehicle)
            {
                if (Vehicle)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        Vehicle.SendMessage("MoveForward");
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        Vehicle.SendMessage("MoveBackward");
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        Vehicle.SendMessage("TurnLeft");
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        Vehicle.SendMessage("TurnRight");
                    }
                }
                if (ShootingBehaviour)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        ShootingBehaviour.SendMessage("Shoot");
                    }
                }
                if (TankTurret)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        TankTurret.TurnAntiClockwise();
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        TankTurret.TurnClockwise();
                    }
                }

                if (CanControlBase)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        ChangingVehicle = true;
                    }
                }
            }
            else
            {
                if (CanControlBase)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        ChangingVehicle = false;
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (Manager.GameEnded)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "End Game");
            string msg;
            if (Winner)
            {
                msg = "Winner!";
            }
            else
            {
                msg = "!Loser";
            }
            GUI.Label(new Rect(Screen.width / 2, Screen.width / 2, 10000, 20), msg);
        }
        else
        {
            if (ChangingVehicle)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Vehicle Selection");

                if (GUI.Button(new Rect(0, 0, Screen.width / 2, Screen.height), "Tank: " + Manager.GetRemainingVehicles(Team, VehicleType.Tank).ToString()) && Manager.GetRemainingVehicles(Team, VehicleType.Jeep) > 0)
                {
                    Manager.ChangePlayerVehicle(this, VehicleType.Tank);
                }

                if (GUI.Button(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height), "Jeep: " + Manager.GetRemainingVehicles(Team, VehicleType.Jeep).ToString()) && Manager.GetRemainingVehicles(Team, VehicleType.Tank) > 0)
                {
                    Manager.ChangePlayerVehicle(this, VehicleType.Jeep);
                }
            }
        }
    }

    public bool Winner { get; set; }

    internal void SetBaseControls(bool status)
    {
        CanControlBase = status;
    }

    internal Camera GetPlayerCamera()
    {
        return Camera.main;
    }

    internal void RemoveCurrentVehicle()
    {
        Manager.RemoveVehicle(Team, Vehicle.Vehicle);
    }
}
