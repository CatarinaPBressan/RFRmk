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
    private MatchManager matchStatus;
    private TankTurretBehaviour tankTurret;

	// Use this for initialization
	void Start () {
        vehicleController = gameObject.GetComponent<VehicleController>() as VehicleController;
        shootingBehaviour = gameObject.GetComponentInChildren<ShootingBehaviour>() as ShootingBehaviour;
        tankTurret = gameObject.GetComponentInChildren<TankTurretBehaviour>() as TankTurretBehaviour;
        matchStatus = Object.FindObjectOfType(typeof(MatchManager)) as MatchManager;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!matchStatus.GameEnded)
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
        }
    }

    void OnGUI()
    {
        if (matchStatus.GameEnded)
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
    }

    public bool Winner { get; set; }
}
