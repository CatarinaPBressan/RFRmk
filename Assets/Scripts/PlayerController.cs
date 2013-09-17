using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Team team;

    public bool canCarryFlag
    {
        get
        {
            if (_vehicleController != null)
            {
                return _vehicleController.canCarryFlag;
            }
            return false;
        }
    }

    private VehicleController _vehicleController;
    private ShootingBehaviour _shootingBehaviour;

	// Use this for initialization
	void Start () {
        _vehicleController = gameObject.GetComponent<VehicleController>() as VehicleController;
        _shootingBehaviour = gameObject.GetComponent<ShootingBehaviour>() as ShootingBehaviour;
	}
	
	// Update is called once per frame
	void Update () {

        if (_vehicleController)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _vehicleController.SendMessage("MoveForward");
            }
            if (Input.GetKey(KeyCode.S))
            {
                _vehicleController.SendMessage("MoveBackward");
            }

            if (Input.GetKey(KeyCode.A))
            {
                _vehicleController.SendMessage("TurnLeft");
            }

            if (Input.GetKey(KeyCode.D))
            {
                _vehicleController.SendMessage("TurnRight");
            }
        }

        if (_shootingBehaviour)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _shootingBehaviour.SendMessage("Shoot");
            }
        }
	
	}
}
