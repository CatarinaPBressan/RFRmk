using UnityEngine;
using System.Collections;

public class TeamBaseBehaviour : MonoBehaviour {

    public Team team;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collisionInfo)
    {
        Debug.Log("baseplate trigger");

        FlagBehaviour fb = collisionInfo.GetComponent<FlagBehaviour>();

        if (fb != null)
        {
            if (fb.team != this.team)
            {
                Destroy(fb.gameObject);
                //
            }
        }
    }
}
