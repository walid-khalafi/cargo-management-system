using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Specifies the type of a waypoint in a transportation route.
    /// </summary>
    public enum WaypointType
    {
        /// Location where goods are loaded.
        Pickup = 0,

        /// Location where goods are unloaded.
        Dropoff = 1,

        /// Fuel station or refueling stop.
        Refuel = 2,

        /// Driver rest stop.
        Rest = 3,

        /// Border crossing point between regions or countries.
        BorderCrossing = 4,

        /// Other intermediate waypoint not covered by the above types.
        Other = 5
    }
}
