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
            DalObject.DalObject dal = new();

            Choice choice;
            bool flag = true;

            Console.WriteLine("Welcome to Drone Deliveries!\n");

            while (flag)
            {
                Console.WriteLine("Pick one of the following options:\n\n" +
                    "ADD 1-4:\n " +
                    "Station-1, Drone-2, Customer-3, Parcel-4.\n\n" +
                    "UPDATE 5-9:\n" +
                    "Parcel to drone-5, Parcel pickup-6, Delivery-7, Drone charge-8 ,Finish charge-9.\n\n" +
                    "DISPLAY 10-13:\n" +
                    "Station-10, Drone-11, Cistomer-12, Parcel-13.\n\n" +
                    "LISTS DISPLAY 14-19:\n" +
                    "Stations-14, Drones-15, Customers-16, Parcels-17," +
                    "Unassosiated parcels-18, Stations with free charge slots-19.\n" +
                    "\nEND-20 ");

                Choice.TryParse(Console.ReadLine(), out choice);

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
                    case Choice.end: flag = false;
                        break;
                }
            }
        }





        public static void Addstation()
        {

            Station st = new();

            int id, chargSolts;
            double longitude, lattitude;

            Console.WriteLine("ENTER Id");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("\nENTER Name");
            st.Name = Console.ReadLine();
            Console.WriteLine("\nENTER Charge slots");
            int.TryParse(Console.ReadLine(), out chargSolts);
            Console.WriteLine("\nENTER longitude");
            double.TryParse(Console.ReadLine(), out longitude);
            Console.WriteLine("\nENTER latitude");
            double.TryParse(Console.ReadLine(), out lattitude);

            st.Id = id; st.ChargeSlots = chargSolts; st.Longitude = longitude; st.Lattitude = lattitude;

            DalObject.DalObject.Addstation(st);
        }

        public static void Adddrone()
        {
            Drone drone = new();
            int droneId;
            IDAL.DO.WeightCategories maxWeight; IDAL.DO.DroneStatuses status;
            double battery;

            Console.WriteLine("ENTER ID\n");
            int.TryParse(Console.ReadLine(), out droneId);
            Console.WriteLine("ENTER Model\n");
            drone.Model = Console.ReadLine();
            Console.WriteLine("ENTER Battery\n");
            double.TryParse(Console.ReadLine(), out battery);
            Console.WriteLine("ENTER MaxWeight\n");
            IDAL.DO.WeightCategories.TryParse(Console.ReadLine(), out maxWeight);
            Console.WriteLine("ENTER Status\n");
            IDAL.DO.DroneStatuses.TryParse(Console.ReadLine(), out status);

            drone.Id = droneId; drone.Battery = battery; drone.MaxWeight = maxWeight; drone.Status = status;
            DalObject.DalObject.AddDrone(drone);

        }

        public static void Addcustomer()
        {
            Customer customer = new();
            int id;
            double longitude, lattitude;

            Console.WriteLine("ENTER Id");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("\nENTER Name");
            customer.Name = Console.ReadLine();
            Console.WriteLine("\nENTER phone");
            customer.Phone = string.Format("{0:###-#######}", Console.ReadLine());
            Console.WriteLine("\nENTER longitude");
            double.TryParse(Console.ReadLine(), out longitude);
            Console.WriteLine("\nENTER latitude");
            double.TryParse(Console.ReadLine(), out lattitude);

            customer.Id = id; customer.Longitude = longitude; customer.Lattitude = lattitude;
            DalObject.DalObject.Addcustumer(customer);
        }

        public static void Addparcel()
        {
            Parcel parcel = new();
            int senderId, targetId;
            IDAL.DO.WeightCategories weight; IDAL.DO.Priorities priority;

            Console.WriteLine("ENTER sender ID\n");
            int.TryParse(Console.ReadLine(), out senderId);
            Console.WriteLine("ENTER Target ID\n");
            int.TryParse(Console.ReadLine(), out targetId);
            Console.WriteLine("ENTER Weight\n");
            IDAL.DO.WeightCategories.TryParse(Console.ReadLine(), out weight);
            Console.WriteLine("ENTER proirity\n");
            IDAL.DO.Priorities.TryParse(Console.ReadLine(), out priority);

            parcel.Senderid = senderId; parcel.TargetId = targetId; parcel.Weight = weight; parcel.Priority = priority;
            parcel.DroneId = 0; parcel.Requested = DateTime.Now;
            DalObject.DalObject.AddParcel(parcel);
        }
        /// <summary>
        /// apply a parcel to a drone
        /// </summary>
        public static void SetParcelToDrone()
        {
            int pid, did;
            Console.WriteLine("ENTER parcel ID\n");
            int.TryParse(Console.ReadLine(), out pid);
            Console.WriteLine("ENTER drone ID\n");
            int did = int.Parse(Console.ReadLine());
            DalObject.DalObject.parcelToDrone(pid, did);
        }
        /// <summary>
        /// updates the time of pickup
        /// </summary>
        public static void ParcelPickup()
        {
            int pid;

            Console.WriteLine("Enter ID of parcel\n");
            int.TryParse(Console.ReadLine(), out pid);

            DalObject.DalObject.UpdatePickup(pid);

        }
        /// <summary>
        /// updates time of delivery
        /// </summary>
        public static void ParcelDelivered()
        {
            int id;

            Console.WriteLine("Enter id of parcel\n");
            int.TryParse(Console.ReadLine(), out id);

            DalObject.DalObject.UpdateDelivery(id);
        }
        /// <summary>
        /// drone finished charging, updates status 
        /// </summary>
        private static void FinishCharge()
        {
            int droneId;

            Console.WriteLine("ENTER id of drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            DalObject.DalObject.EndCharge(droneId);
        }
        /// <summary>
        /// display parcel
        /// </summary>
        public static void ParcelDisplay()
        {
            int parcelId;

            Console.WriteLine("Enter ID of the Parcel\n");
            int.TryParse(Console.ReadLine(), out parcelId);

            Console.WriteLine(DalObject.DalObject.GetParcel(parcelId).ToString());
        }
        /// <summary>
        /// charging a drone, updates status to charging
        /// </summary>
        public static void DroneCharge()
        {
            int droneId, stationId;

            Console.WriteLine("ENTER id of drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            Console.WriteLine("pick a station to charge and enter station id\n");
            stationsWithFreeSlotsDisplay();
            int sid = int.Parse(Console.ReadLine());
            DalObject.DalObject.chargeDrone(did, sid);
        }
        /// <summary>
        /// display customer
        /// </summary>
        public static void CustomerDisplay()
        {
            int customerId;

            Console.WriteLine("Enter ID of the costumer\n");
            int.TryParse(Console.ReadLine(), out customerId);

            Console.WriteLine(DalObject.DalObject.GetCostumer(customerId).ToString());
        }
        /// <summary>
        /// dispay drone
        /// </summary>
        public static void DroneDisplay()
        {
            int droneId;

            Console.WriteLine("Enter ID of the drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            Console.WriteLine(DalObject.DalObject.GetDrone(droneId).ToString());
        }
        /// <summary>
        /// display station
        /// </summary>
        public static void StationDisplay()
        {
            int stationId;

            Console.WriteLine("Enter ID of the staiton\n");
            int.TryParse(Console.ReadLine(), out stationId);

            Console.WriteLine(DalObject.DalObject.GetStation(stationId).ToString());
            return;
        }
        
        public static void stationListDisplay()
        {
            foreach (var Station in DalObject.DalObject.stationList())
            {
                Console.WriteLine(Station.ToString());
            
        }
        /// <summary>
        /// display the list of drones
        /// </summary>
        public static void DroneListDisplay()
        {
            foreach (var Drone in DalObject.DalObject.droneList())
            {
                Console.WriteLine(Drone.ToString());
            
        }
        /// <summary>
        /// display the list of customers
        /// </summary>
        public static void CustomerListDisplay()
        {
            foreach (var Customer in DalObject.DalObject.customerList())
            {
                Console.WriteLine(Customer.ToString());
            
        }
        /// <summary>
        /// display the list of parcels
        /// </summary>
        public static void ParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.parcelList())
            {
                Console.WriteLine(Parcel.ToString());
            
        }
        /// <summary>
        /// display list of stations with free charge slots
        /// </summary>
        public static void StationsWithFreeSlotsDisplay()
        {
            foreach (var Station in DalObject.DalObject.stationList())
            {
                if(Station.ChargeSlots != 0)
                     Console.WriteLine(Station.ToString());
            }
        }
       
        public static void unassosiatedParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.parcelList())
            {
                if(Parcel.DroneId != 0)
                    Console.WriteLine(Parcel.ToString());
            
        }
    }
}
