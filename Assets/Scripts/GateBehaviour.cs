using UnityEngine;
using System.Collections;

public class GateBehaviour : MonoBehaviour
{
    public GameObject ViewGO;
    public Team Team;

    void Start()
    {
        if (!ViewGO)
        {
            Debug.LogError("A gate should have a GameObject to open");
        }
    }

    public void Open(Collider enterer)
    {
        PlayerController pc = enterer.gameObject.GetComponent<PlayerController>();
        if (!pc)
        {
            return;
        }
        Team entererTeam = pc.Team;
        if (entererTeam.Equals(Team))
        {
            ViewGO.transform.Translate(Vector3.up * 10);
        }
    }

    public void Close(Collider exiter)
    {
        PlayerController pc = exiter.gameObject.GetComponent<PlayerController>();
        if (!pc)
        {
            return;
        }
        Team exiterTeam = pc.Team;
        if (exiterTeam.Equals(Team))
        {
            ViewGO.transform.Translate(-Vector3.up * 10);
        }
    }
}