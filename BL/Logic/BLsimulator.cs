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
        BL blClass;
        public const double speed = 100;
        public const int timer = 1000;
        private Drone drone;
        double FlightDistance;
        Action updateDrone;
        DalApi.Parcel parcel;
        Location location;

        public Simulator(int droneId, Action update, Func<bool> finish, IBL bl)
        {
            blClass = (BL)bl;
            drone = blClass.GetDrone(droneId);
            updateDrone = update;


            do
            {
                switch (drone.Status)
                {
                    case DroneStatuses.free:
                        lock (bl)
                        {
                            DroneActionBySituation(blClass.SimulatorParcelToDrone(droneId));
                            update();
                        }
                        break;

                    case DroneStatuses.maintenance:
                        {
                            do
                            {
                                Thread.Sleep(timer);
                                drone.BatteryStatus += BL.ChargePace;
                                if (drone.BatteryStatus > 100)
                                    drone.BatteryStatus = 100;
                                update();
                            } while (drone.BatteryStatus < 100);

                            blClass.ReleaseDrone(droneId);
                            update();
                        }
                        break;

                    case DroneStatuses.sending:
                        {
                            parcel = blClass.dal.GetParcel(drone.Parcel.Id);

                            lock (bl)
                                if (blClass.GetDroneSituation(droneId) == "Associated")
                                    DroneActionBySituation("Is associating");

                                else
                                {
                                    MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                                    blClass.ParcelProvisionUpdate(drone.Id);
                                }
                            update();
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
                        location = blClass.SenderLocation(parcel);
                        MovingDrone(blClass.LocationsDistance(drone.CurrentLocation, location), BL.FreeElectricityUse);
                        blClass.ParcelPickedupUptade(drone.Id);
                        MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                        blClass.ParcelProvisionUpdate(drone.Id);
                    }
                    break;

                case "Not enough battery":
                    {
                        lock (blClass)
                        {
                            location = blClass.StationLocation(blClass.StationForCharging(drone.Id));
                            MovingDrone(blClass.LocationsDistance(drone.CurrentLocation, location), BL.FreeElectricityUse);
                            blClass.ChargeDrone(drone.Id);// send to charge
                        }
                        //sleep the time from location to charge station                                
                    }
                    break;

                case "No Parcels for delivery":
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
                drone.BatteryStatus -= batteryUse;
                updateDrone();
            }
            Thread.Sleep(timer);
            drone.BatteryStatus -= batteryUse * distance;
            updateDrone();
        }
    }
}
