using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TeamStatus
{
    public int Score
    {
        get;
        private set;
    }

    private Dictionary<VehicleType, int> RemainingVehicles;
    
    public TeamStatus(int[] remainingVehicles = null)
    {
        Score = 0;
        RemainingVehicles = new Dictionary<VehicleType, int>();
        foreach (var vehicleType in Utils.Vehicles)
        {
            RemainingVehicles.Add(vehicleType, 5);
        }
        if (remainingVehicles != null)
        {
            RemainingVehicles[VehicleType.Jeep] = remainingVehicles[0];
            if (remainingVehicles.Length > 1)
            {
                RemainingVehicles[VehicleType.Tank] = remainingVehicles[1];
            }
        }
    }

    public void RemoveVehicle(VehicleType vehicle)
    {
        RemainingVehicles[vehicle]--;
    }

    internal int GetRemainingVehiclesOfType(VehicleType vehicle)
    {
        return RemainingVehicles[vehicle];
    }

    internal int GetRemainingVehicles()
    {
        int total = 0;
        foreach (var vehicleType in Utils.Vehicles)
        {
            total += RemainingVehicles[vehicleType];
        }
        return total;
    }

    internal bool HasVehiclesLeft()
    {
        if (GetRemainingVehicles() <= 0)
        {
            return false;
        }
        return true;
    }

    internal void AddScore(int amount = 1)
    {
        Score += amount;
    }
}
