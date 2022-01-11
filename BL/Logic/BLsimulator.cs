using System.Threading;
using System;
using BlApi;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BO
{
    internal class Simulator
    {
        BL blClass;
        public const double speed = 100;
        public const int timer = 1000;
        public static Drone Drone;
        DalApi.Parcel parcel;
        Location location;
        event Action Update;

        public Simulator(int droneId, Action update, Func<bool> finish, IBL bl)
        {
            blClass = (BL)bl;
            Drone = blClass.GetDrone(droneId);
            Update = update;

            do
            {
                Update();
                switch (Drone.Status)
                {
                    case DroneStatuses.free:
                        {
                            DroneActionBySituation(blClass.SimulatorParcelToDrone(droneId));
                            Drone = blClass.GetDrone(droneId);
                        }
                        break;

                    case DroneStatuses.maintenance:
                        {
                            Update();
                            while (Drone.BatteryStatus + BL.ChargePace < 100)
                            {                                
                                Drone.BatteryStatus += BL.ChargePace;
                                Thread.Sleep(timer);
                            }

                            blClass.ReleaseDrone(droneId);
                            Drone = blClass.GetDrone(droneId);
                            Thread.Sleep(timer);
                        }
                        break;

                    case DroneStatuses.sending:
                        {
                            if (blClass.GetDroneSituation(droneId) == "Associated")
                                DroneActionBySituation("Is associating");

                            else
                            {
                                MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                                blClass.ParcelProvisionUpdate(Drone.Id);
                                Drone = blClass.GetDrone(Drone.Id);
                                Thread.Sleep(timer);
                            }
                        }
                        break;
                }
            } while (finish());
        }

        private void DroneActionBySituation(string situation)
        {
            Update();
            switch (situation)
            {                
                case "Is associating":
                    {
                        Drone = blClass.GetDrone(Drone.Id);
                        parcel = blClass.dal.GetParcel(Drone.Parcel.Id);
                        Thread.Sleep(timer);
                        location = blClass.SenderLocation(parcel);
                        MovingDrone(blClass.LocationsDistance(Drone.CurrentLocation, location), BL.FreeElectricityUse);
                        blClass.ParcelPickedupUptade(Drone.Id);
                        Drone = blClass.GetDrone(Drone.Id);
                        Thread.Sleep(timer);
                        MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                        blClass.ParcelProvisionUpdate(Drone.Id);
                        Drone = blClass.GetDrone(Drone.Id);
                        Thread.Sleep(timer);
                    }
                    break;

                case "Not enough battery":
                    {
                        lock (blClass)
                            location = blClass.StationLocation(blClass.StationForCharging(Drone.Id));
                        MovingDrone(blClass.LocationsDistance(Drone.CurrentLocation, location), BL.FreeElectricityUse);
                        blClass.ChargeDrone(Drone.Id);// send to charge
                        Drone = blClass.GetDrone(Drone.Id);                        
                        Thread.Sleep(timer);
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
            Update();
            while (distance - speed > 0)
            {
                distance -= speed;
                Drone.BatteryStatus -= batteryUse * speed;
                Thread.Sleep(timer);
            }

            Drone.BatteryStatus -= batteryUse * distance;
            Thread.Sleep(timer);
        }


    }
}
