using System.Runtime.CompilerServices;
using BL;
using System.Threading;
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
                    try
                    {
                        bl.ParcelToDrone(droneId);
                        Thread.Sleep(timer); update();
                        bl.ParcelPickedupUptade(droneId);
                        Thread.Sleep(timer); update();
                        bl.ParcelProvisionUpdate(droneId);
                        Thread.Sleep(timer); update();
                    }
                    catch (Exception)
                    {
                        try { bl.ChargeDrone(droneId); }
                        catch (Exception)
                        {//what to do if not enouph battery to reach station }
                        }
                    }
                    if (status == DroneStatuses.maintenance)
                    {
                        bl.ReleaseDrone(droneId);
                        Thread.Sleep(timer); update();
                    }
                    if (status == DroneStatuses.sending)
                    {
                        bl.ParcelProvisionUpdate(droneId);
                        Thread.Sleep(timer); update();
                    }
                }
            } while (finish());
        }
    }
}
