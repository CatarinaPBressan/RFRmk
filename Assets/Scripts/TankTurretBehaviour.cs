using UnityEngine;
using System.Collections;

public class TankTurretBehaviour : MonoBehaviour {

    public float TurnSpeed = 1f;
    public bool flipped = false;

    private GameObject turretGO;

    void Start()
    {
        turretGO = this.gameObject;
    }

    internal void TurnAntiClockwise(bool force = false)
    {
        if (flipped && !force)
        {
            TurnClockwise(true);
        }
        else
        {
            turretGO.transform.Rotate(Vector3.up * TurnSpeed, Space.Self);
        }
    }

    internal void TurnClockwise(bool force = false)
    {
        if (flipped && !force)
        {
            TurnAntiClockwise(true);
        }
        else
        {
            turretGO.transform.Rotate(Vector3.down * TurnSpeed, Space.Self);
        }
    }//gustavosjc
}
