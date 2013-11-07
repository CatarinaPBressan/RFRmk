using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Team Team;

    public bool CanCarryFlag
    {
        get
        {
            if (vehicleController != null)
            {
                return vehicleController.canCarryFlag;
            }
            return false;
        }
    }

    private VehicleController vehicleController;
    private ShootingBehaviour shootingBehaviour;
    private MatchManager matchManager;
    private TankTurretBehaviour tankTurret;

    private bool canControlBase = false;
    private bool changingVehicle = false;

	void Start () 
    {
        vehicleController = gameObject.GetComponent<VehicleController>() as VehicleController;
        shootingBehaviour = gameObject.GetComponentInChildren<ShootingBehaviour>() as ShootingBehaviour;
        tankTurret = gameObject.GetComponentInChildren<TankTurretBehaviour>() as TankTurretBehaviour;
        matchManager = Object.FindObjectOfType(typeof(MatchManager)) as MatchManager;
	}
	
    void Update()
    {
        if (!matchManager.GameEnded)
        {
            if (!changingVehicle)
            {
                if (vehicleController)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        vehicleController.SendMessage("MoveForward");
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        vehicleController.SendMessage("MoveBackward");
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        vehicleController.SendMessage("TurnLeft");
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        vehicleController.SendMessage("TurnRight");
                    }
                }
                if (shootingBehaviour)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        shootingBehaviour.SendMessage("Shoot");
                    }
                }
                if (tankTurret)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        tankTurret.TurnAntiClockwise();
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        tankTurret.TurnClockwise();
                    }
                }

                if (canControlBase)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        changingVehicle = true;
                    }
                }
            }
            else
            {
                if (canControlBase)
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        changingVehicle = false;
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (matchManager.GameEnded)
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
            if (changingVehicle)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Vehicle Selection");

                if (GUI.Button(new Rect(0, 0, Screen.width / 2, Screen.height), "Tank: " + matchManager.GetRemainingVehicles(Team, VehicleType.Tank).ToString()) && matchManager.GetRemainingVehicles(Team, VehicleType.Jeep) > 0)
                {
                    matchManager.ChangePlayerVehicle(this, VehicleType.Tank);
                }

                if (GUI.Button(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height), "Jeep: " + matchManager.GetRemainingVehicles(Team, VehicleType.Jeep).ToString()) && matchManager.GetRemainingVehicles(Team, VehicleType.Tank) > 0)
                {
                    matchManager.ChangePlayerVehicle(this, VehicleType.Jeep);
                }
            }
        }
    }

    public bool Winner { get; set; }

    internal void SetBaseControls(bool status)
    {
        canControlBase = status;
    }

    internal Camera GetPlayerCamera()
    {
        return Camera.main;
    }
}
