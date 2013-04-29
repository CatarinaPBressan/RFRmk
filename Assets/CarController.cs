using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down);
            this.transform.Translate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up);
            this.transform.Translate(Vector3.forward);
        }
	}
}
