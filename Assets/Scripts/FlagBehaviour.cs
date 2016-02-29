using UnityEngine;
using System.Collections;

public class FlagBehaviour : MonoBehaviour {

    public Team team = Team.Brown;

    private bool IsBeingCarried = false;

	void Update () {
        if (IsBeingCarried)
        {
            this.transform.position = this.transform.parent.position;
        }
	}

    void OnCollisionEnter(Collision enterer)
    {
        if (!IsBeingCarried)
        {
            PlayerController pc = enterer.gameObject.GetComponent<PlayerController>() as PlayerController;

            if (pc != null && pc.CanCarryFlag && !pc.Team.Equals(this.team))
            {
                Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), enterer.gameObject.GetComponent<Collider>());
                this.transform.position = enterer.gameObject.transform.position;
                this.transform.parent = enterer.gameObject.transform;
                IsBeingCarried = true;
            }
        }
    } 
}
