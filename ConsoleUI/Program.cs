using System;
using IDAL.DO;
using DalObject;

namespace ConsoleUI
{
     class Program
    {
        public enum Choice
        {
            addStation = 1, addDrone, addCustomer, addParcel,
            parcelToDrone, pickup, delivery, droneCharge, finishCharge,
            stationDisplay, droneDisplay, customerDisplay, parcelDisplay,
            stationList, droneList, customerList, parcelList, unassosiatedParcelList, stationsWithFreeSlots,
            customerDistance, stationDistance,
            end
        }

        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new();

            bool flag = true;

            Console.WriteLine("Welcome to Drone Deliveries!\n");

            while (flag)
            {
                Console.WriteLine("\n\nPick one of the following options:\n\n" +
               "Add 1-4:\n " +
               "Station-1, Drone-2, Customer-3, Parcel-4.\n\n" +
               "Update 5-9:\n" +
               "Parcel to drone-5, Parcel pickup-6, Delivery-7, Drone charge-8 ,Finish charge-9.\n\n" +
               "Display 10-13:\n" +
               "Station-10, Drone-11, Cistomer-12, Parcel-13.\n\n" +
               "Display Lists 14-19:\n" +
               "Station list-14, Drone list-15, Customer list-16, Parcel list-17,\n" +
               "Unassosiated parcels-18, Stations with free charge slots-19.\n" +
               "Distances 21-22\n:" +
               "Point to customer-20, Point to station-21\n\n" +
               "END-22 ");

                Enum.TryParse(Console.ReadLine(), out Choice choice);

                switch (choice)
                {
                    case Choice.addStation:
                        AddStation(dalObject);
                        break;
                    case Choice.addDrone:
                        AddDrone(dalObject);
                        break;
                    case Choice.addCustomer:
                        AddCustomer(dalObject);
                        break;
                    case Choice.addParcel:
                        AddParcel(dalObject);
                        break;
                    case Choice.parcelToDrone:
                        SetParcelToDrone(dalObject);
                        break;
                    case Choice.stationDisplay:
                        StationDisplay(dalObject);
                        break;
                    case Choice.droneDisplay:
                        DroneDisplay(dalObject);
                        break;
                    case Choice.customerDisplay:
                        CustomerDisplay(dalObject);
                        break;
                    case Choice.parcelDisplay:
                        ParcelDisplay(dalObject);
                        break;
                    case Choice.pickup:
                        ParcelPickup(dalObject);
                        break;
                    case Choice.delivery:
                        ParcelDelivered(dalObject);
                        break;
                    case Choice.droneCharge:
                        DroneCharge(dalObject);
                        break;
                    case Choice.finishCharge:
                        FinishCharge(dalObject);
                        break;
                    case Choice.stationList:
                        StationListDisplay(dalObject);
                        break;
                    case Choice.droneList:
                        DroneListDisplay(dalObject);
                        break;
                    case Choice.customerList:
                        CustomerListDisplay(dalObject);
                        break;
                    case Choice.parcelList:
                        ParcelListDisplay(dalObject);
                        break;
                    case Choice.unassosiatedParcelList:
                        UnassosiatedParcelListDisplay(dalObject);
                        break;
                    case Choice.stationsWithFreeSlots:
                        StationsWithFreeSlotsDisplay(dalObject);
                        break;
                    case Choice.customerDistance:
                        CustomerDistance(dalObject);
                        break;
                    case Choice.stationDistance:
                        StationDistance(dalObject);
                        break;
                    case Choice.end:
                        flag = false;
                        break;
                }
            }
        }


        /// <summary>
        /// distance between user poin and station
        /// </summary>
        /// <param name="dalObject"></param>
        private static void StationDistance(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nENTER longitude");
            double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER lattitude");
            double.TryParse(Console.ReadLine(), out double lattitude);
            Console.WriteLine("\nENTER Station ID");
            int.TryParse(Console.ReadLine(), out int customerId);

            Console.WriteLine("The distance between the point to station is:\n"
                + dalObject.DistanceCalculate(lattitude, longitude,
                dalObject.GetStation(customerId).Lattitude, dalObject.GetStation(customerId).Longitude));
        }


        /// <summary>
        /// distance between user point and customer
        /// </summary>
        /// <param name="dalObject"></param>
        public static void CustomerDistance(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nENTER longitude");
            double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER lattitude");
            double.TryParse(Console.ReadLine(), out double lattitude);
            Console.WriteLine("\nENTER Customer ID");
            int.TryParse(Console.ReadLine(), out int customerId);

            Console.WriteLine("The distance between the point to customer is:\n"
                + dalObject.DistanceCalculate(lattitude, longitude, 
                dalObject.GetCustomer(customerId).Lattitude, dalObject.GetCustomer(customerId).Longitude));
            
        }


        /// <summary>
        /// add a station
        /// </summary>
        public static void AddStation(DalObject.DalObject dalObject)
        {
            Station station = new();

            Console.WriteLine("ENTER Id");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Name");
            station.Name = Console.ReadLine();
            Console.WriteLine("\nENTER Charge slots");
            _ = int.TryParse(Console.ReadLine(), out int chargeSlots);
            Console.WriteLine("\nENTER longitude");
            _ = double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER latitude");
            _ = double.TryParse(Console.ReadLine(), out double latitude);

            station.Id = id; station.ChargeSlots = chargeSlots; station.Longitude = longitude; station.Lattitude = latitude;
            dalObject.AddStation(station);
        }


        /// <summary>
        /// add a drone
        /// </summary>
        public static void AddDrone(DalObject.DalObject dalObject)
        {
            Drone drone = new();

            Console.WriteLine("ENTER Id\n");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Model");
            drone.Model = Console.ReadLine();
            Console.WriteLine("\nENTER Battery");
            _ = double.TryParse(Console.ReadLine(), out double battery);
            Console.WriteLine("\nENTER MaxWeight");
            _ = Enum.TryParse(Console.ReadLine(), out WeightCategories maxWeight);
            Console.WriteLine("\nENTER Status");
            _ = Enum.TryParse(Console.ReadLine(), out DroneStatuses status);

            drone.Id = id; drone.Battery = battery; drone.MaxWeight = maxWeight; drone.Status = status;
            dalObject.AddDrone(drone);

        }


        /// <summary>
        /// adds a customer
        /// </summary>
        public static void AddCustomer(DalObject.DalObject dalObject)
        {
            Customer customer = new();

            Console.WriteLine("\nENTER Id");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Name");
            customer.Name = Console.ReadLine();
            Console.WriteLine("\nENTER phone");
            customer.Phone = Console.ReadLine();
            Console.WriteLine("\nENTER longitude");
            _ = double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER latitude");
            _ = double.TryParse(Console.ReadLine(), out double lattitude);

            customer.Id = id; customer.Longitude = longitude; customer.Lattitude = lattitude;
            dalObject.Addcustumer(customer);

        }


        /// <summary>
        /// adds a parcel
        /// </summary>
        public static void AddParcel(DalObject.DalObject dalObject)
        {
            Parcel parcel = new();
            Console.WriteLine("\nENTER sender ID");
            _ = int.TryParse(Console.ReadLine(), out int senderId);
            Console.WriteLine("\nENTER Target ID");
            _ = int.TryParse(Console.ReadLine(), out int TargetId);
            Console.WriteLine("\nENTER Weight");
            _ = Enum.TryParse(Console.ReadLine(), out WeightCategories weight);
            Console.WriteLine("\nENTER proirity");
            _ = Enum.TryParse(Console.ReadLine(), out Priorities priority);
            parcel.DroneId = 0;
            parcel.Requested = DateTime.Now;

            parcel.Senderid = senderId; parcel.TargetId = TargetId; parcel.Weight = weight; parcel.Priority = priority;
            dalObject.AddParcel(parcel);
        }


        /// <summary>
        /// apply a parcel to a drone
        /// </summary>
        public static void SetParcelToDrone(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nENTER parcel ID");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);
            Console.WriteLine("\nENTER drone ID");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            dalObject.ParcelToDrone(parcelId, droneId);
        }


        /// <summary>
        /// updates the time of pickup
        /// </summary>
        public static void ParcelPickup(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nEnter ID of parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

           dalObject.UpdatePickup(parcelId);
        }


        /// <summary>
        /// updates time of delivery
        /// </summary>
        public static void ParcelDelivered(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nEnter id of parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            dalObject.UpdateDelivery(parcelId);
        }


        /// <summary>
        /// drone finished charging, updates status 
        /// </summary>
        private static void FinishCharge(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nENTER id of drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            dalObject.EndCharge(droneId);
        }


        /// <summary>
        /// display parcel
        /// </summary>
        public static void ParcelDisplay(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nEnter ID of the Parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            Console.WriteLine(dalObject.GetParcel(parcelId).ToString());
        }


        /// <summary>
        /// charging a drone, updates status to charging
        /// </summary>
        public static void DroneCharge(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nENTER id of drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);
            Console.WriteLine("\npick a station to charge and enter station id");
            StationsWithFreeSlotsDisplay(dalObject);
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            dalObject.ChargeDrone(droneId, stationId);
        }


        /// <summary>
        /// display customer
        /// </summary>
        public static void CustomerDisplay(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nEnter ID of the costumer");
            _ = int.TryParse(Console.ReadLine(), out int id);

            Console.WriteLine(dalObject.GetCustomer(id).ToString());
        }


        /// <summary>
        /// dispay drone
        /// </summary>
        public static void DroneDisplay(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nEnter ID of the drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            Console.WriteLine(dalObject.GetDrone(droneId).ToString());
        }


        /// <summary>
        /// display station
        /// </summary>
        public static void StationDisplay(DalObject.DalObject dalObject)
        {
            Console.WriteLine("\nEnter ID of the staiton");
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            Console.WriteLine(dalObject.GetStation(stationId).ToString());
        }


        /// <summary>
        /// display the list of stations
        /// </summary>
        public static void StationListDisplay(DalObject.DalObject dalObject)
        {
            foreach (var Station in dalObject.StationList())
                Console.WriteLine(Station.ToString());

        }


        /// <summary>
        /// display the list of Drones
        /// </summary>
        public static void DroneListDisplay(DalObject.DalObject dalObject)
        {
            foreach (var Drone in dalObject.DroneList())
                Console.WriteLine(Drone.ToString());

        }


        /// <summary>
        /// display the list of Customers
        /// </summary>
        public static void CustomerListDisplay(DalObject.DalObject dalObject)
        {
            foreach (var Customer in dalObject.CustomerList())
                Console.WriteLine(Customer.ToString());

        }


        /// <summary>
        /// display the list of parcels
        /// </summary>
        public static void ParcelListDisplay(DalObject.DalObject dalObject)
        {
            foreach (var Parcel in dalObject.ParcelList())
                Console.WriteLine(Parcel.ToString());

        }


        /// <summary>
        /// display list of stations with free charge slots
        /// </summary>
        public static void StationsWithFreeSlotsDisplay(DalObject.DalObject dalObject)
        {
            foreach (var Station in dalObject.StationList())
                if (Station.ChargeSlots != 0)
                    Console.WriteLine(Station.ToString());
        }


        /// <summary>
        /// display list pf unassosiated parcels
        /// </summary>
        public static void UnassosiatedParcelListDisplay(DalObject.DalObject dalObject)
        {
            foreach (var Parcel in dalObject.ParcelList())
                if (Parcel.DroneId != 0)
                    Console.WriteLine(Parcel.ToString());
        }       
    }
}