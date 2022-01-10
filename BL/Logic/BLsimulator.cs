using System.Threading;
using System;
using BlApi;

namespace BO
{
    internal class Simulator
    {
        BL blClass;
        public const double speed = 100;
        public const int timer = 1000;
        public static Drone Drone;
        //double Battery;
        Action updateDrone;
        DalApi.Parcel parcel;
        Location location;

        public Simulator(int droneId, Action update, Func<bool> finish, IBL bl)
        {
            blClass = (BL)bl;
            Drone = blClass.GetDrone(droneId);
            updateDrone = update;
            //Battery = drone.BatteryStatus;

            do
            {
                switch (Drone.Status)
                {
                    case DroneStatuses.free:
                        lock (bl)
                            DroneActionBySituation(blClass.SimulatorParcelToDrone(droneId));
                        break;

                    case DroneStatuses.maintenance:
                        {
                            do
                            {

                                if (Drone.BatteryStatus + BL.ChargePace > 100)
                                    Drone.BatteryStatus = 100;
                                else
                                    Drone.BatteryStatus += BL.ChargePace;

                                updateDrone();
                                Thread.Sleep(timer);

                            } while (Drone.BatteryStatus < 100);

                            blClass.ReleaseDrone(droneId);
                            Drone = blClass.GetDrone(droneId);
                            updateDrone();
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
                        Drone = blClass.GetDrone(Drone.Id);
                        parcel = blClass.dal.GetParcel(Drone.Parcel.Id);
                        updateDrone();
                        Thread.Sleep(timer);
                        location = blClass.SenderLocation(parcel);
                        MovingDrone(blClass.LocationsDistance(Drone.CurrentLocation, location), BL.FreeElectricityUse);
                        blClass.ParcelPickedupUptade(Drone.Id);
                        Drone = blClass.GetDrone(Drone.Id);
                        updateDrone();
                        Thread.Sleep(timer);
                        MovingDrone(blClass.SenderTaregetDistance(parcel), blClass.dal.BatteryUseRequest()[(int)parcel.Weight]);
                        blClass.ParcelProvisionUpdate(Drone.Id);
                        Drone = blClass.GetDrone(Drone.Id);
                        updateDrone();
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
                        updateDrone();
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
            while (distance - speed > 0)
            {
                distance -= speed;
                Drone.BatteryStatus -= batteryUse * speed;
                updateDrone();
                Thread.Sleep(timer);
            }

            Drone.BatteryStatus -= batteryUse * distance;
            updateDrone();
            Thread.Sleep(timer);
        }
    }
}
