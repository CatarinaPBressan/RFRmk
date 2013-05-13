using UnityEngine;
using System.Collections;

public class FlagBehaviour : MonoBehaviour {

    public Team team;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        VehicleController vc = collisionInfo.gameObject.GetComponent<VehicleController>();

        /*if (vc != null &&)
        {
            hc.Damage(this.Damage);
            Debug.Log("damaged!");
        }*/

        Destroy(this.gameObject);
    } 
}
