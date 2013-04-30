using UnityEngine;
using System.Collections;

public class VehicleController : MonoBehaviour {


    public enum MovementType
    {
        Wheels,
        Treads
    }

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
	}
	
	// Update is called once per frame
	void Update () {
        CheckMoving();
        Debug.Log("isMoving =" + isMoving);

        if (Input.GetKey(KeyCode.W))
        {
            this.rigidbody.AddForce(this.transform.forward * forwardPower);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.rigidbody.AddForce(-this.transform.forward * forwardPower);
            //this.transform.Translate(Vector3.back);
        }
        
        if (this.movementType == MovementType.Wheels)
        {
            if (Input.GetKey(KeyCode.A) && isMoving)
            {
                this.rigidbody.AddTorque(Vector3.down * (this.rigidbody.angularDrag + turningPower));
                //this.transform.Rotate(Vector3.down);
            }

            if (Input.GetKey(KeyCode.D) && isMoving)
            {
                this.rigidbody.AddTorque(Vector3.up * (this.rigidbody.angularDrag + turningPower));
                //this.transform.Rotate(Vector3.up);
            }
        }

        if (this.movementType == MovementType.Treads)
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.rigidbody.AddTorque(Vector3.down * (this.rigidbody.angularDrag + turningPower));
                //this.transform.Rotate(Vector3.down);
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.rigidbody.AddTorque(Vector3.up * (this.rigidbody.angularDrag + turningPower));
                //this.transform.Rotate(Vector3.up);
            }
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
