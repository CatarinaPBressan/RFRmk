using UnityEngine;
using System.Collections;

public class TeamBaseBehaviour : MonoBehaviour {

    public Team team;

    private MatchManager mm;

    void Start()
    {
        mm = Object.FindObjectOfType(typeof(MatchManager)) as MatchManager;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        FlagBehaviour fb = collisionInfo.GetComponent<FlagBehaviour>();
        if (fb != null && fb.team != this.team)
        {
            Destroy(fb.gameObject);
            mm.Score(team);
        }
    }
}
