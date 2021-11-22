using System;
using IDAL.DO;
using IDAL;

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
            IDal iDal= new DalObject();

            bool flag = true;

            Console.WriteLine("Welcome to Drone Deliveries!");

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
                try
                {
                    switch (choice)
                    {
                        case Choice.addStation:
                            AddStation(iDal);
                            break;
                        case Choice.addDrone:
                            AddDrone(iDal);
                            break;
                        case Choice.addCustomer:
                            AddCustomer(iDal);
                            break;
                        case Choice.addParcel:
                            AddParcel(iDal);
                            break;
                        case Choice.parcelToDrone:
                            SetParcelToDrone(iDal);
                            break;
                        case Choice.stationDisplay:
                            StationDisplay(iDal);
                            break;
                        case Choice.droneDisplay:
                            DroneDisplay(iDal);
                            break;
                        case Choice.customerDisplay:
                            CustomerDisplay(iDal);
                            break;
                        case Choice.parcelDisplay:
                            ParcelDisplay(iDal);
                            break;
                        case Choice.pickup:
                            ParcelPickup(iDal);
                            break;
                        case Choice.delivery:
                            ParcelDelivered(iDal);
                            break;
                        case Choice.droneCharge:
                            DroneCharge(iDal);
                            break;
                        case Choice.finishCharge:
                            FinishCharge(iDal);
                            break;
                        case Choice.stationList:
                            StationListDisplay(iDal);
                            break;
                        case Choice.droneList:
                            DroneListDisplay(iDal);
                            break;
                        case Choice.customerList:
                            CustomerListDisplay(iDal);
                            break;
                        case Choice.parcelList:
                            ParcelListDisplay(iDal);
                            break;
                        case Choice.unassosiatedParcelList:
                            UnassosiatedParcelListDisplay(iDal);
                            break;
                        case Choice.stationsWithFreeSlots:
                            StationsWithFreeSlotsDisplay(iDal);
                            break;
                        case Choice.customerDistance:
                            CustomerDistance(iDal);
                            break;
                        case Choice.stationDistance:
                            StationDistance(iDal);
                            break;
                        case Choice.end:
                            flag = false;
                            break;
                    }
                }
                catch (IDAL.AddExistException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    
                }
                catch(IDAL.ItemNotFoundException ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }
        
	     
        /// <summary>
                /// distance between user poin and station
                /// </summary>
                /// <param name="dalObject"></param>
        private static void StationDistance(IDAL.IDal iDal)
        {
            Console.WriteLine("\nENTER longitude");
            double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER lattitude");
            double.TryParse(Console.ReadLine(), out double latitude);
            Console.WriteLine("\nENTER Station ID");
            int.TryParse(Console.ReadLine(), out int customerId);

            Console.WriteLine("The distance between the point to station is:\n"
                + iDal.DistanceCalculate(latitude, longitude,
                iDal.GetStation(customerId).Latitude, iDal.GetStation(customerId).Longitude));
        }


        /// <summary>
        /// distance between user point and customer
        /// </summary>
        /// <param name="dalObject"></param>
        public static void CustomerDistance(IDAL.IDal iDal)
        {
            Console.WriteLine("\nENTER longitude");
            double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER lattitude");
            double.TryParse(Console.ReadLine(), out double latitude);
            Console.WriteLine("\nENTER Customer ID");
            int.TryParse(Console.ReadLine(), out int customerId);

            Console.WriteLine("The distance between the point to customer is:\n"
                + iDal.DistanceCalculate(latitude, longitude,
                iDal.GetCustomer(customerId).Latitude, iDal.GetCustomer(customerId).Longitude));

        }


        /// <summary>
        /// add a station
        /// </summary>
        public static void AddStation(IDAL.IDal iDal)
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

            station.Id = id; station.ChargeSlots = chargeSlots; station.Longitude = longitude; station.Latitude = latitude;
            iDal.AddStation(station);
        }


        /// <summary>
        /// add a drone
        /// </summary>
        public static void AddDrone(IDAL.IDal iDal)
        {
            Drone drone = new();

            Console.WriteLine("ENTER Id\n");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Model");
            drone.Model = Console.ReadLine();
            Console.WriteLine("\nENTER MaxWeight");
            _ = Enum.TryParse(Console.ReadLine(), out WeightCategories maxWeight);

            drone.Id = id; drone.MaxWeight = maxWeight;
            iDal.AddDrone(drone);

        }


        /// <summary>
        /// adds a customer
        /// </summary>
        public static void AddCustomer(IDAL.IDal iDal)
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

            customer.Id = id; customer.Longitude = longitude; customer.Latitude = lattitude;
            iDal.AddCustumer(customer);

        }


        /// <summary>
        /// adds a parcel
        /// </summary>
        public static void AddParcel(IDAL.IDal iDal)
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
            iDal.AddParcel(parcel);
        }


        /// <summary>
        /// apply a parcel to a drone
        /// </summary>
        public static void SetParcelToDrone(IDAL.IDal iDal)
        {
            Console.WriteLine("\nENTER parcel ID");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);
            Console.WriteLine("\nENTER drone ID");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            iDal.ParcelToDrone(parcelId, droneId);
        }


        /// <summary>
        /// updates the time of pickup
        /// </summary>
        public static void ParcelPickup(IDAL.IDal dalObject)
        {
            Console.WriteLine("\nEnter ID of parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            dalObject.UpdatePickup(parcelId);
        }


        /// <summary>
        /// updates time of delivery
        /// </summary>
        public static void ParcelDelivered(IDAL.IDal dalObject)
        {
            Console.WriteLine("\nEnter id of parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            dalObject.UpdateDelivery(parcelId);
        }


        /// <summary>
        /// drone finished charging, updates status 
        /// </summary>
        private static void FinishCharge(IDAL.IDal dalObject)
        {
            Console.WriteLine("\nENTER id of drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            dalObject.EndCharge(droneId);
        }


        /// <summary>
        /// display parcel
        /// </summary>
        public static void ParcelDisplay(IDAL.IDal dalObject)
        {
            Console.WriteLine("\nEnter ID of the Parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            Console.WriteLine(dalObject.GetParcel(parcelId).ToString());
        }


        /// <summary>
        /// charging a drone, updates status to charging
        /// </summary>
        public static void DroneCharge(IDAL.IDal dalObject)
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
        public static void CustomerDisplay(IDAL.IDal dalObject)
        {
            Console.WriteLine("\nEnter ID of the costumer");
            _ = int.TryParse(Console.ReadLine(), out int id);

            Console.WriteLine(dalObject.GetCustomer(id).ToString());
        }


        /// <summary>
        /// dispay drone
        /// </summary>
        public static void DroneDisplay(IDAL.IDal Dal)
        {
            Console.WriteLine("\nEnter ID of the drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            Console.WriteLine(Dal.GetDrone(droneId).ToString());
        }


        /// <summary>
        /// display station
        /// </summary>
        public static void StationDisplay(IDAL.IDal iDal)
        {
            try
            {
                Console.WriteLine("\nEnter ID of the staiton");
                _ = int.TryParse(Console.ReadLine(), out int stationId);

                Console.WriteLine(iDal.GetStation(stationId).ToString());
            }
            catch(IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
            }          
        }


        /// <summary>
        /// display the list of stations
        /// </summary>
        public static void StationListDisplay(IDAL.IDal iDal)
        {
            foreach (var Station in iDal.StationList())
                Console.WriteLine(Station.ToString());

        }


        /// <summary>
        /// display the list of Drones
        /// </summary>
        public static void DroneListDisplay(IDAL.IDal iDal)
        {
            foreach (var Drone in iDal.DroneList())
                Console.WriteLine(Drone.ToString());

        }


        /// <summary>
        /// display the list of Customers
        /// </summary>
        public static void CustomerListDisplay(IDAL.IDal iDal)
        {
            foreach (var Customer in iDal.CustomerList())
                Console.WriteLine(Customer.ToString());

        }


        /// <summary>
        /// display the list of parcels
        /// </summary>
        public static void ParcelListDisplay(IDAL.IDal iDal)
        {
            foreach (var Parcel in iDal.ParcelList())
                Console.WriteLine(Parcel.ToString());

        }


        /// <summary>
        /// display list of stations with free charge slots
        /// </summary>
        public static void StationsWithFreeSlotsDisplay(IDAL.IDal iDal)
        {            
            foreach (var Station in iDal.StationsWithFreeSlots())                
                    Console.WriteLine(Station.ToString());
        }


        /// <summary>
        /// display list pf unassosiated parcels
        /// </summary>
        public static void UnassosiatedParcelListDisplay(IDAL.IDal iDal)
        {
            foreach (var Parcel in iDal.ParcelList())
                if (Parcel.DroneId != 0)
                    Console.WriteLine(Parcel.ToString());
        }
    }
}


    

  