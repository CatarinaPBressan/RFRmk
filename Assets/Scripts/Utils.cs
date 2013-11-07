using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Utils
{
    public static Team[] Teams
    {
        get
        {
            return new Team[] { Team.Brown, Team.Green };
        }
        private set
        {
            return;
        }
    }

    public static VehicleType[] Vehicles
    {
        get
        {
            return new VehicleType[] { VehicleType.Jeep, VehicleType.Tank };
        }
        private set
        {
            return;
        }
    }
}
