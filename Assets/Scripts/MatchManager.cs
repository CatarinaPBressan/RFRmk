using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    public int RequiredCapturesToWin = 1;
    public GameObject JeepPrefab;
    public GameObject TankPrefab;

    private Dictionary<Team,int> TeamScore;
    private Dictionary<Team, TeamStatus> TeamStatuses;
    private List<PlayerController> Players;
    

	void Start () {
        TeamScore = new Dictionary<Team, int>();
        TeamStatuses = new Dictionary<Team, TeamStatus>();
        foreach (var team in Utils.Teams)
        {
            TeamScore.Add(team, 0);
            TeamStatuses.Add(team, new TeamStatus());
        }
        Players = new List<PlayerController>();
        Players.AddRange(Object.FindObjectsOfType(typeof(PlayerController)) as PlayerController[]);
	}

    internal void Score(Team team)
    {
        TeamScore[team]++;
        if (TeamScore[team] == RequiredCapturesToWin)
        {
            EndGame(team);
        }
    }

    private void EndGame(Team team)
    {
        foreach (var player in Players)
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
        Players.Remove(playerController);
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
        Players.Add(instancePlayerController);
    }

    internal int GetRemainingVehicles(Team Team, VehicleType vehicleType)
    {
        return TeamStatuses[Team].GetRemainingVehiclesOfType(vehicleType);
    }

    internal void RemoveVehicle(Team Team, VehicleType vehicleType)
    {
        TeamStatuses[Team].RemoveVehicle(vehicleType);
    }
}
