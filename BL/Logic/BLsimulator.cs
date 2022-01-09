using System.Runtime.CompilerServices;
using BL;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;
using BO;
using System.Threading;

namespace BO
{
    internal class Simulator
    {
        BL blClass;
        public const double speed = 100;
        public const int timer = 1000;
        private Drone drone;
        //double Battery;
        Action updateDrone;
        DalApi.Parcel parcel;
        Location location;

        public Simulator(int droneId, Action update, Func<bool> finish, IBL bl)
        {
            blClass = (BL)bl;
            drone = blClass.GetDrone(droneId);
            updateDrone = update;
            //Battery = drone.BatteryStatus;

            do
            {
                switch (drone.Status)
                {
                    case DroneStatuses.free:
                        lock (bl)
                            DroneActionBySituation(blClass.SimulatorParcelToDrone(droneId));
                        break;

                    case DroneStatuses.maintenance:
                        {
                            do
                            {
                                Thread.Sleep(timer);

                                if (drone.BatteryStatus + BL.ChargePace > 100)
                                    drone.BatteryStatus = 100;
                                else
                                    drone.BatteryStatus += BL.ChargePace;

                                blClass.BatteryUpdate(droneId, drone.BatteryStatus);

                                updateDrone();
                            } while (drone.BatteryStatus < 100);

                            blClass.ReleaseDrone(droneId);
                            drone = blClass.GetDrone(droneId);
                            updateDrone();
                        }
                        break;

                    case DroneStatuses.sending:
                        {
                            

                            lock (bl)
                                if (blClass.GetDroneSituation(droneId) == "Associated")
                                    DroneActionBySituation("Is associating");

                                else
                                {
                                    MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                                    blClass.ParcelProvisionUpdate(drone.Id);
                                    updateDrone();
                                }
                        }
                        break;
                }
            } while (finish());
        }

        private void DroneActionBySituation(string situation)
        {
            switch (situation)
            {
                case "Is associating":
                    {
                        drone = blClass.GetDrone(drone.Id);
                        parcel = blClass.dal.GetParcel(drone.Parcel.Id);
                        
                        updateDrone();
                        location = blClass.SenderLocation(parcel);
                        MovingDrone(blClass.LocationsDistance(drone.CurrentLocation, location), BL.FreeElectricityUse);
                        blClass.ParcelPickedupUptade(drone.Id);
                        Thread.Sleep(timer);
                        updateDrone();
                        MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                        blClass.ParcelProvisionUpdate(drone.Id);
                        drone = blClass.GetDrone(drone.Id);
                        Thread.Sleep(timer);
                        updateDrone();
                    }
                    break;

                case "Not enough battery":
                    {
                        lock (blClass)
                            location = blClass.StationLocation(blClass.StationForCharging(drone.Id));
                        MovingDrone(blClass.LocationsDistance(drone.CurrentLocation, location), BL.FreeElectricityUse);
                        blClass.ChargeDrone(drone.Id);// send to charge
                        drone = blClass.GetDrone(drone.Id);
                        updateDrone();
                        //sleep the time from location to charge station                                
                    }
                    break;

                case "No match Parcel for delivery":
                    {
                        Thread.Sleep(timer);
                    }
                    break;
            }
        }

        private void MovingDrone(double distance, double batteryUse)
        {
            while (distance - speed > 0)
            {
                Thread.Sleep(timer);
                distance -= speed;
                drone.BatteryStatus -= batteryUse * speed;
                blClass.BatteryUpdate(drone.Id, drone.BatteryStatus);
                updateDrone();
            }

            Thread.Sleep(timer);
            drone.BatteryStatus -= batteryUse * distance;
            blClass.BatteryUpdate(drone.Id, drone.BatteryStatus);
            updateDrone();
        }
    }
}
