using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    public int RequiredCapturesToWin = 1;
    public GameObject JeepPrefab;
    public GameObject TankPrefab;
    public bool GameEnded
    {
        get;
        private set;
    }


    private Dictionary<Team, TeamStatus> TeamStatus;
    private List<PlayerController> Players;

    void Start()
    {
        TeamStatus = new Dictionary<Team, TeamStatus>();
        foreach (var team in Utils.Teams)
        {
            TeamStatus.Add(team, new TeamStatus());
        }
        Players = new List<PlayerController>();
        Players.AddRange(Object.FindObjectsOfType(typeof(PlayerController)) as PlayerController[]);
    }

    internal void Score(Team team)
    {
        TeamStatus[team].AddScore();
        if (TeamStatus[team].Score == RequiredCapturesToWin)
        {
            EndGame(team);
        }
    }

    private void EndGame(Team Winner)
    {
        foreach (var player in Players)
        {
            if (player.Team.Equals(Winner))
            {
                player.Winner = true;
            }
        }
        GameEnded = true;
    }

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

    internal int GetRemainingVehiclesOfType(Team Team, VehicleType vehicleType)
    {
        return TeamStatus[Team].GetRemainingVehiclesOfType(vehicleType);
    }

    internal void RemoveVehicle(Team Team, VehicleType vehicleType)
    {
        TeamStatus[Team].RemoveVehicle(vehicleType);
    }
}
