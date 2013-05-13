using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {


    

    public float forwardPower = 100;
    public float turningPower = 25;
    public MovementType movementType = MovementType.Wheels;


    private bool isMoving = false;
    private SmoothFollow sf;
    private float initialCamHeight;
    
    // Use this for initialization
	void Start () {
        sf = Camera.mainCamera.GetComponent<SmoothFollow>() as SmoothFollow;
        initialCamHeight = sf.height;
        //initialCamDistance = sf.distance;

	}

    void Update()
    {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckMoving();
        //Debug.Log("this.rigidbody.velocity.sqrMagnitude =" + this.rigidbody.velocity.sqrMagnitude);

        if (Input.GetKey(KeyCode.W))
        {
            this.rigidbody.AddForce(this.transform.forward * (this.rigidbody.mass * forwardPower));
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.rigidbody.AddForce(-this.transform.forward * (this.rigidbody.mass * forwardPower));
        }
        
        if (Input.GetKey(KeyCode.A) && (isMoving || this.movementType == MovementType.Treads))
        {
            this.rigidbody.AddTorque(Vector3.down * (this.rigidbody.angularDrag + turningPower) * this.rigidbody.mass);
        }

        if (Input.GetKey(KeyCode.D) && (isMoving || this.movementType == MovementType.Treads))
        {
            this.rigidbody.AddTorque(Vector3.up * (this.rigidbody.angularDrag + turningPower) * this.rigidbody.mass);
        }
    
        sf.height = this.rigidbody.velocity.magnitude + initialCamHeight;

	}


    private void CheckMoving()
    {
        if (this.rigidbody.velocity.sqrMagnitude < 4)
        {
            isMoving = false;
            return;
        }
        isMoving = true;
    }
}
