using UnityEngine;
using System.Collections;

public class TankTurretBehaviour : MonoBehaviour {

    public float TurnSpeed = 1f;
    public bool Flipped = false;

    private GameObject Turret;

    void Start()
    {
        Turret = this.gameObject;
    }

    internal void TurnAntiClockwise(bool force = false)
    {
        if (Flipped && !force)
        {
            TurnClockwise(true);
        }
        else
        {
            Turret.transform.Rotate(Vector3.up * TurnSpeed, Space.Self);
        }
    }

    internal void TurnClockwise(bool force = false)
    {
        if (Flipped && !force)
        {
            TurnAntiClockwise(true);
        }
        else
        {
            Turret.transform.Rotate(Vector3.down * TurnSpeed, Space.Self);
        }
    }
}
