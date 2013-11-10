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

    private void EndGame(Team Team, bool Win = true)
    {
        foreach (var player in Players)
        {
            if (player.Team.Equals(Team))
            {
                if (Win)
                {
                    player.Winner = true;
                }
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
        Debug.Log("tanks: " + TeamStatus[Team].GetRemainingVehiclesOfType(VehicleType.Tank) + " Jeeps: " + TeamStatus[Team].GetRemainingVehiclesOfType(VehicleType.Jeep));
        if (TeamStatus[Team].HasVehiclesLeft())
        {
            PlayerController playerController = null;
            foreach (var player in Players)
            {
                if (player.Team.Equals(Team))
                {
                    playerController = player;
                    break;
                }
            }
            Players.Remove(playerController);
            SmoothFollow sf = playerController.GetPlayerCamera().GetComponent<SmoothFollow>() as SmoothFollow;
            GameObject newVehicle = null;
            if (TeamStatus[Team].GetRemainingVehiclesOfType(VehicleType.Jeep) > 0)
            {
                newVehicle = JeepPrefab;
            }
            else if (TeamStatus[Team].GetRemainingVehiclesOfType(VehicleType.Tank) > 0)
            {
                newVehicle = TankPrefab;
            }

            var bases = GameObject.FindObjectsOfType(typeof(TeamBaseBehaviour)) as TeamBaseBehaviour[];
            Transform teamBaseTransform = null;
            foreach (var teamBase in bases)
            {
                if (teamBase.Team.Equals(Team))
                {
                    teamBaseTransform = teamBase.transform;
                    break;
                }
            }
            var instance = GameObject.Instantiate(newVehicle, teamBaseTransform.position, new Quaternion()) as GameObject;
            PlayerController instancePlayerController = instance.GetComponentInChildren<PlayerController>() as PlayerController;
            instancePlayerController.Team = playerController.Team;
            sf.target = instancePlayerController.gameObject.transform;
            Players.Add(instancePlayerController);
        }
        else
        {
            EndGame(Team, false);
            return;
        }
    }
}
