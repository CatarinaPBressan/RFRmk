using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    public int RequiredCapturesToWin = 1;

    private Dictionary<Team,int> teamScore;
    private PlayerController[] playerControllers;


	// Use this for initialization
	void Start () {
        teamScore = new Dictionary<Team, int>();
        teamScore.Add(Team.Brown, 0);
        teamScore.Add(Team.Green, 0);
        playerControllers = Object.FindObjectsOfType(typeof(PlayerController)) as PlayerController[];
	}
	
	// Update is called once per frame
	void Update () {
	
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
        foreach (var player in playerControllers)
        {
            if (player.Team.Equals(team))
            {
                player.Winner = true;
            }
        }

        GameEnded = true;
    }

    public bool GameEnded { get; private set; }
}
