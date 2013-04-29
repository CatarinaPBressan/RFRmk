using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    //public float speed = 0.5f;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            this.rigidbody.AddForce(this.transform.forward);
            //this.transform.Translate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up);
        }

        if (this.rigidbody.velocity.magnitude > 0 && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            this.rigidbody.AddForce(-this.rigidbody.velocity.x * 0.2f, -this.rigidbody.velocity.y * 0.2f, -this.rigidbody.velocity.z * 0.2f);
        }
	}
}
