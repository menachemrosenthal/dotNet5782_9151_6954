using System.Runtime.CompilerServices;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;
using BO;

namespace BO
{
    internal class Simulator
    {
        public static int speed = 100;
        public static int timer = 1000;
        
        public Simulator(int droneId, Action update, Func<bool> finish, IBL bl)
        {
            Drone drone = bl.GetDrone(droneId);
            DroneStatuses status;

            do
            {
                status = drone.Status;
                if (status == DroneStatuses.free)
                {
                    bl.ParcelToDrone(droneId);
                }
                if (status == DroneStatuses.maintenance)
                {
                    
                }
                if (status == DroneStatuses.sending)
                {

                }

            } while (finish());
        }
    }
}
