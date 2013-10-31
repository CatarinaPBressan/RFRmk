using UnityEngine;
using System.Collections;

public class TeamBaseBehaviour : MonoBehaviour {

    public Team team;

    private MatchManager matchManager;

    void Start()
    {
        matchManager = Object.FindObjectOfType(typeof(MatchManager)) as MatchManager;
    }

    void OnTriggerEnter(Collider enterer)
    {
        FlagBehaviour fb = enterer.GetComponent<FlagBehaviour>();
        if (fb != null && fb.team != this.team)
        {
            Destroy(fb.gameObject);
            matchManager.Score(team);
        }

        PlayerController pc = enterer.GetComponent<PlayerController>();

        if (pc != null && pc.Team == this.team)
        {
            pc.SetBaseControls(true);
        }
    }

    void OnTriggerExit(Collider exiter)
    {
        PlayerController pc = exiter.GetComponent<PlayerController>();

        if (pc != null && pc.Team == this.team)
        {
            pc.SetBaseControls(false);
        }
    }
}
