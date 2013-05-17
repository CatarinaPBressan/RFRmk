using UnityEngine;
using System.Collections;

public class FlagTower : MonoBehaviour {

    public bool hasFlag = false;
    public Team team = Team.Brown;

    public GameObject flagGO;
    private bool isQuitting = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()
    {
        isQuitting = true;
    }


    void OnDestroy()
    {
        if (!isQuitting)
        {
            if (hasFlag)
            {
                GameObject flag = GameObject.Instantiate(flagGO, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
                FlagBehaviour fb = flag.GetComponent<FlagBehaviour>();

                fb.team = this.team;

            }
        }
    }
}
