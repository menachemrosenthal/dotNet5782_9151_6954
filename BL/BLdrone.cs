using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public IEnumerable<object> GetDroneList()
        {
            return Drones;
        }
       
        public void ParcelPickedupUptade(int droneId)
        {
            if(DroneStatus(droneId) == "Associated")
            {
                DroneToList drone = new();
                drone = Drones.Find(x => x.Id == droneId);
                double distance = LocationsDistance(drone.CurrentLocation, SenderLocation(dal.GetParcel(drone.DeliveredParcelId)));
                drone.BatteryStatus -= distance * FreeElectricityUse;
                drone.CurrentLocation = SenderLocation(dal.GetParcel(drone.DeliveredParcelId));
                dal.UpdatePickup(drone.DeliveredParcelId);
                return;
            }
            //throw..
        }

        public void AddDrone(DroneToList drone, int stationID)
        {
            Random r = new Random();
            drone.BatteryStatus = r.Next(20, 40);
            drone.CurrentLocation = new();
            drone.CurrentLocation = StationLocation(dal.GetStation(stationID));
            drone.Status = Enums.DroneStatuses.maintenance;
            IDAL.DO.Drone daldrone = new();
            daldrone.Id = drone.Id; daldrone.Model = drone.Model;
            daldrone.MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight;
            dal.AddDrone(daldrone);
            Drones.Add(drone);
            dal.ChargeDrone(drone.Id, stationID);
        }
        
        public void DroneNameUpdate(int droneId, string updateName)
        {
            IDAL.DO.Drone drone = new();
            drone = dal.DroneList().First(x => x.Id == droneId);
            drone.Model = updateName;
            dal.DroneUpdate(drone);
        }

        public void ChargeDrone(int droneId)
        {
            if (DroneStatus(droneId) == "Free")
            {
                DroneToList drone = new();
                drone = Drones.Find(x => x.Id == droneId);
                int index = Drones.IndexOf(drone);

                //possible station location
                Location location = new();
                location = StationLocation(ClosestStation(drone.CurrentLocation, dal.StationsWithFreeSlots()));

                double distance = LocationsDistance(drone.CurrentLocation, location);

                if (FreeElectricityUse * distance >= drone.BatteryStatus)
                {
                    drone.CurrentLocation = location;
                    drone.BatteryStatus -= FreeElectricityUse * distance;
                    drone.Status = Enums.DroneStatuses.maintenance;
                    Drones[index] = drone;
                    dal.ChargeDrone(droneId, ClosestStation(location, dal.StationList()).Id);

                    return;
                }
            }

            //throw ...
        }

        public void ReleaseDrone(int droneId, TimeSpan time)
        {
            DroneToList drone = new();
            drone = Drones.Find(x => x.Id == droneId);
            if (drone.Status == Enums.DroneStatuses.maintenance)
            {
                double timeToDouble = time.Minutes;
                timeToDouble /= 60;
                drone.BatteryStatus = ChargePace * timeToDouble;
                if (drone.BatteryStatus > 100)
                    drone.BatteryStatus = 100;

                drone.Status = Enums.DroneStatuses.free;
                dal.EndCharge(droneId);
                int index = Drones.IndexOf(drone);
                Drones[index] = drone;
            }
            //else
            //    throw
        }

        public void ParcelToDrone(int droneId)
        {
            if (DroneStatus(droneId) == "Free")
            {
                DroneToList drone = new();
                drone = Drones.Find(x => x.Id == droneId);

                List<IDAL.DO.Parcel> parcels = new();
                parcels = SortParcels(drone.CurrentLocation);
                
                int weight = (int)drone.MaxWeight;

                foreach (var parcel in parcels)
                {
                    if(weight >= (int)parcel.Weight)
                    {
                        if(drone.BatteryStatus >= BatteryUseInDelivery(drone,parcel))
                        {
                            drone.Status = Enums.DroneStatuses.sending;
                            drone.DeliveredParcelId = parcel.Id;
                            dal.ParcelToDrone(parcel.Id, drone.Id);
                            Drones[Drones.IndexOf(drone)] = drone;
                            return;
                        }
                        
                    }
                }
            }

            //therow...;
        }

        public Drone getDrone(int DroneId)
        {
            Drone drone = new();
            drone.Id = DroneId;
            DroneToList d = new(); d = Drones.Find(x => x.Id == DroneId);
            drone.Model = d.Model; drone.MaxWeight = d.MaxWeight;
            drone.Status = d.Status;drone.BatteryStatus = d.BatteryStatus;
            drone.CurrentLocation = d.CurrentLocation;
            if (drone.Status == Enums.DroneStatuses.sending)
            {
                drone.Parcel = getParcelInTransfer(d.DeliveredParcelId);
            }
            return drone;
        }


        //                   **************        Auxiliary functions          ***************           //

        /// <summary>
        /// cheking drone status
        /// </summary>
        /// <param name="id">drone id for check</param>
        /// <returns>"Free" or "Associated" or "Executing"</returns>
        string DroneStatus(int id)
        {
            if (!dal.ParcelList().Any(x => x.DroneId == id))
                return "Free";
            else
            {
                IDAL.DO.Parcel parcel = new();
                parcel = dal.ParcelList().First(x => x.DroneId == id);

                if (parcel.PickedUp == null)
                    return "Associated";
                if (parcel.Delivered == null)
                    return "Executing";
            }
            return "Free";
        }

        double BatteryUseInDelivery(DroneToList drone, IDAL.DO.Parcel parcel)
        {            
            double BatteryUse = LocationsDistance(drone.CurrentLocation, SenderLocation(parcel)) * FreeElectricityUse;
            BatteryUse += SenderTaregetDistance(parcel) * dal.BatteryUseRquest()[(int)parcel.Weight];
            BatteryUse += CustomerClosestStationDistance(parcel.TargetId) * FreeElectricityUse;

            return BatteryUse;
        }
    }
}
