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
            end
        }





        static void Main(string[] args)
        {
            DalObject.DalObject _ = new();

            bool flag = true;

            Console.WriteLine("Welcome to Drone Deliveries!\n");

            while (flag)
            {
                Console.WriteLine("Pick one of the following options:\n\n" +
               "Add 1-4:\n " +
               "Station-1, Drone-2, Customer-3, Parcel-4.\n\n" +
               "Update 5-9:\n" +
               "Parcel to drone-5, Parcel pickup-6, Delivery-7, Drone charge-8 ,Finish charge-9.\n\n" +
               "Display 10-13:\n" +
               "Station-10, Drone-11, Cistomer-12, Parcel-13.\n\n" +
               "Display Lists 14-19:\n" +
               "Station list-14, Drone list-15, Customer list-16, Parcel list-17,\n" +
               "Unassosiated parcels-18, Stations with free charge slots-19.\n\n" +
               "END-20 ");
                    
                Enum.TryParse(Console.ReadLine(), out Choice choice);

                switch (choice)
                {
                    case Choice.addStation:
                        Addstation();
                        break;
                    case Choice.addDrone:
                        AddDrone();
                        break;
                    case Choice.addCustomer:
                        AddCustomer();
                        break;
                    case Choice.addParcel:
                        AddParcel();
                        break;
                    case Choice.parcelToDrone:
                        SetParcelToDrone();
                        break;
                    case Choice.stationDisplay:
                        StationDisplay();
                        break;
                    case Choice.droneDisplay:
                        DroneDisplay();
                        break;
                    case Choice.customerDisplay:
                        CustomerDisplay();
                        break;
                    case Choice.parcelDisplay:
                        ParcelDisplay();
                        break;
                    case Choice.pickup:
                        ParcelPickup();
                        break;
                    case Choice.delivery:
                        ParcelDelivered();
                        break;
                    case Choice.droneCharge:
                        DroneCharge();
                        break;
                    case Choice.finishCharge:
                        FinishCharge();
                        break;
                    case Choice.stationList:
                        StationListDisplay();
                        break;
                    case Choice.droneList:
                        DroneListDisplay();
                        break;
                    case Choice.customerList:
                        CustomerListDisplay();
                        break;
                    case Choice.parcelList:
                        ParcelListDisplay();
                        break;
                    case Choice.unassosiatedParcelList:
                        UnassosiatedParcelListDisplay();
                        break;
                    case Choice.stationsWithFreeSlots:
                        StationsWithFreeSlotsDisplay();
                        break;
                    case Choice.end:
                        flag = false;
                        break;

                }
            }
        }




        /// <summary>
        /// adds a station
        /// </summary>
        public static void Addstation()
        {
            Station st = new();
            Console.WriteLine("ENTER Id");
            st.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("\nENTER Name");
            st.Name = Console.ReadLine();
            Console.WriteLine("\nENTER Charge slots");
            st.ChargeSlots = int.Parse(Console.ReadLine());
            Console.WriteLine("\nENTER longitude");
            st.Longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("\nENTER latitude");
            st.Lattitude = double.Parse(Console.ReadLine());
            DalObject.DalObject.AddStation(st);
        }


        /// <summary>
        /// addsa drone
        /// </summary>
        public static void AddDrone()
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
            DalObject.DalObject.AddDrone(drone);

        }


        /// <summary>
        /// adds a customer
        /// </summary>
        public static void AddCustomer()
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
            DalObject.DalObject.Addcustumer(customer);

        }


        /// <summary>
        /// adds a parcel
        /// </summary>
        public static void AddParcel()
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
            DalObject.DalObject.AddParcel(parcel);
        }


        /// <summary>
        /// apply a parcel to a drone
        /// </summary>
        public static void SetParcelToDrone()
        {
            Console.WriteLine("\nENTER parcel ID");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);
            Console.WriteLine("\nENTER drone ID");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            DalObject.DalObject.ParcelToDrone(parcelId, droneId);
        }


        /// <summary>
        /// updates the time of pickup
        /// </summary>
        public static void ParcelPickup()
        {
            Console.WriteLine("\nEnter ID of parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            DalObject.DalObject.UpdatePickup(parcelId);
        }


        /// <summary>
        /// updates time of delivery
        /// </summary>
        public static void ParcelDelivered()
        {
            Console.WriteLine("\nEnter id of parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            DalObject.DalObject.UpdateDelivery(parcelId);
        }


        /// <summary>
        /// drone finished charging, updates status 
        /// </summary>
        private static void FinishCharge()
        {
            Console.WriteLine("\nENTER id of drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            DalObject.DalObject.EndCharge(droneId);
        }


        /// <summary>
        /// display parcel
        /// </summary>
        public static void ParcelDisplay()
        {
            Console.WriteLine("\nEnter ID of the Parcel");
            _ = int.TryParse(Console.ReadLine(), out int parcelId);

            Console.WriteLine(DalObject.DalObject.GetParcel(parcelId).ToString());
        }


        /// <summary>
        /// charging a drone, updates status to charging
        /// </summary>
        public static void DroneCharge()
        {
            Console.WriteLine("\nENTER id of drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);
            Console.WriteLine("\npick a station to charge and enter station id");
            StationsWithFreeSlotsDisplay();
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            DalObject.DalObject.ChargeDrone(droneId, stationId);
        }


        /// <summary>
        /// display customer
        /// </summary>
        public static void CustomerDisplay()
        {
            Console.WriteLine("\nEnter ID of the costumer");
            _ = int.TryParse(Console.ReadLine(), out int id);

            Console.WriteLine(DalObject.DalObject.GetCostumer(id).ToString());
        }


        /// <summary>
        /// dispay drone
        /// </summary>
        public static void DroneDisplay()
        {
            Console.WriteLine("\nEnter ID of the drone");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            Console.WriteLine(DalObject.DalObject.GetDrone(droneId).ToString());
        }


        /// <summary>
        /// display station
        /// </summary>
        public static void StationDisplay()
        {
            Console.WriteLine("\nEnter ID of the staiton");
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            Console.WriteLine(DalObject.DalObject.GetStation(stationId).ToString());
        }


        /// <summary>
        /// display the list of stations
        /// </summary>
        public static void StationListDisplay()
        {
            foreach (var Station in DalObject.DalObject.StationList())
                Console.WriteLine(Station.ToString());

        }


        /// <summary>
        /// display the list of drones
        /// </summary>
        public static void DroneListDisplay()
        {
            foreach (var Drone in DalObject.DalObject.DroneList())
                Console.WriteLine(Drone.ToString());

        }


        /// <summary>
        /// display the list of customers
        /// </summary>
        public static void CustomerListDisplay()
        {
            foreach (var Customer in DalObject.DalObject.CustomerList())
                Console.WriteLine(Customer.ToString());

        }


        /// <summary>
        /// display the list of parcels
        /// </summary>
        public static void ParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.ParcelList())
                Console.WriteLine(Parcel.ToString());

        }


        /// <summary>
        /// display list of stations with free charge slots
        /// </summary>
        public static void StationsWithFreeSlotsDisplay()
        {
            foreach (var Station in DalObject.DalObject.StationList())
                if (Station.ChargeSlots != 0)
                    Console.WriteLine(Station.ToString());
        }


        /// <summary>
        /// display list pf unassosiated parcels
        /// </summary>
        public static void UnassosiatedParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.ParcelList())
                if (Parcel.DroneId != 0)
                    Console.WriteLine(Parcel.ToString());
        }
    }
}