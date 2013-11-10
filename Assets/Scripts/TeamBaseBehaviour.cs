using UnityEngine;
using System.Collections;

public class TeamBaseBehaviour : MonoBehaviour {

    public Team Team;

    private MatchManager Manager;

    void Start()
    {
        Manager = Object.FindObjectOfType(typeof(MatchManager)) as MatchManager;
    }

    void OnTriggerEnter(Collider enterer)
    {
        FlagBehaviour fb = enterer.GetComponent<FlagBehaviour>();
        if (fb != null && fb.team != Team)
        {
            Destroy(fb.gameObject);
            Manager.Score(Team);
        }

        PlayerController pc = enterer.GetComponent<PlayerController>();

        if (pc != null && pc.Team == Team)
        {
            pc.SetBaseControls(true);
        }
    }

    void OnTriggerExit(Collider exiter)
    {
        PlayerController pc = exiter.GetComponent<PlayerController>();

        if (pc != null && pc.Team == Team)
        {
            pc.SetBaseControls(false);
        }
    }
}
