using UnityEngine;
using System.Collections;

public class TankTurretBehaviour : MonoBehaviour {

    public float TurnSpeed = 1f;

    private GameObject turretGO;

	// Use this for initialization
    void Start()
    {
        turretGO = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    internal void TurnAntiClockwise()
    {
        turretGO.transform.Rotate(Vector3.up * TurnSpeed, Space.Self);
    }

    internal void TurnClockwise()
    {
        turretGO.transform.Rotate(Vector3.down * TurnSpeed, Space.Self);
    }
}
