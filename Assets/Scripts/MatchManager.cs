using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    public int RequiredCapturesToWin = 1;
    public GameObject JeepPrefab;
    public GameObject TankPrefab;


    private Dictionary<Team,int> teamScore;
    private List<PlayerController> players;




	void Start () {
        teamScore = new Dictionary<Team, int>();
        teamScore.Add(Team.Brown, 0);
        teamScore.Add(Team.Green, 0);
        players = new List<PlayerController>();
        players.AddRange(Object.FindObjectsOfType(typeof(PlayerController)) as PlayerController[]);
	}

    internal void Score(Team team)
    {
        teamScore[team]++;

        if (teamScore[team] == RequiredCapturesToWin)
        {
            EndGame(team);
        }
    }

    private void EndGame(Team team)
    {
        foreach (var player in players)
        {
            if (player.Team.Equals(team))
            {
                player.Winner = true;
            }
        }

        GameEnded = true;
    }

    public bool GameEnded { get; private set; }

    internal void ChangePlayerVehicle(PlayerController playerController, VehicleType vehicleType)
    {
        players.Remove(playerController);
        SmoothFollow sf = playerController.GetPlayerCamera().GetComponent<SmoothFollow>() as SmoothFollow;
        Transform playerTransform = playerController.gameObject.transform;
        GameObject instance = null;
        switch (vehicleType)
        {
            case VehicleType.Jeep:
                instance = GameObject.Instantiate(JeepPrefab, playerTransform.position, playerTransform.rotation) as GameObject;
                break;
            case VehicleType.Tank:
                instance = GameObject.Instantiate(TankPrefab, playerTransform.position, playerTransform.rotation) as GameObject;
                break;
        }
        DestroyImmediate(playerController.gameObject);
        PlayerController instancePlayerController = instance.GetComponentInChildren<PlayerController>() as PlayerController;
        instancePlayerController.Team = playerController.Team;
        sf.target = instancePlayerController.gameObject.transform;
        players.Add(instancePlayerController);
    }
}
