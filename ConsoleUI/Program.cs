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
                        stationDisplay();
                        break;
                    case Choice.droneDisplay:
                        DroneDisplay();
                        break;
                    case Choice.customerDisplay:
                        customerDisplay();
                        break;
                    case Choice.parcelDisplay:
                        ParcelDisplay();
                        break;
                    case Choice.pickup:
                        parcelPickup();
                        break;
                    case Choice.delivery:
                        parcelDelivered();
                        break;
                    case Choice.droneCharge:
                        droneCharge();
                        break;
                    case Choice.finishCharge:
                        finishCharge();
                        break;
                    case Choice.stationList:
                        stationListDisplay();
                        break;
                    case Choice.droneList:
                        droneListDisplay();
                        break;
                    case Choice.customerList:
                        customerListDisplay();
                        break;
                    case Choice.parcelList:
                        parcelListDisplay();
                        break;
                    case Choice.unassosiatedParcelList:
                        unassosiatedParcelListDisplay();
                        break;
                    case Choice.stationsWithFreeSlots:
                        stationsWithFreeSlotsDisplay();
                        break;
                    case Choice.end: flag = false;
                        break;
                }
            }
        }




        /// <summary>
        /// add station into the stations array
        /// </summary>
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

            DalObject.DalObject.AddStation(st);
        }

        /// <summary>
        /// add drone into the drones array
        /// </summary>
        public static void AddDrone()
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

        public static void AddCustomer()
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

        public static void AddParcel()
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

        public static void SetParcelToDrone()
        {
            int pid, did;
            Console.WriteLine("ENTER parcel ID\n");
            int.TryParse(Console.ReadLine(), out pid);
            Console.WriteLine("ENTER drone ID\n");
            int.TryParse(Console.ReadLine(), out did);

            DalObject.DalObject.ParcelToDrone(pid, did);
        }

        public static void parcelPickup()
        {
            int pid;

            Console.WriteLine("Enter ID of parcel\n");
            int.TryParse(Console.ReadLine(), out pid);

            DalObject.DalObject.UpdatePickup(pid);

        }

        public static void parcelDelivered()
        {
            int id;

            Console.WriteLine("Enter id of parcel\n");
            int.TryParse(Console.ReadLine(), out id);

            DalObject.DalObject.UpdateDelivery(id);
        }

        private static void finishCharge()
        {
            int droneId;

            Console.WriteLine("ENTER id of drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            DalObject.DalObject.EndCharge(droneId);
        }

        public static void ParcelDisplay()
        {
            int parcelId;

            Console.WriteLine("Enter ID of the Parcel\n");
            int.TryParse(Console.ReadLine(), out parcelId);

            Console.WriteLine(DalObject.DalObject.GetParcel(parcelId).ToString());
        }

        public static void droneCharge()
        {
            int droneId, stationId;

            Console.WriteLine("ENTER id of drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            Console.WriteLine("pick a station to charge and enter station id\n");
            stationsWithFreeSlotsDisplay();
            int.TryParse(Console.ReadLine(), out stationId);

            DalObject.DalObject.ChargeDrone(droneId, stationId);
        }

        public static void customerDisplay()
        {
            int customerId;

            Console.WriteLine("Enter ID of the costumer\n");
            int.TryParse(Console.ReadLine(), out customerId);

            Console.WriteLine(DalObject.DalObject.GetCostumer(customerId).ToString());
        }

        public static void DroneDisplay()
        {
            int droneId;

            Console.WriteLine("Enter ID of the drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            Console.WriteLine(DalObject.DalObject.GetDrone(droneId).ToString());
        }

        public static void stationDisplay()
        {
            int stationId;

            Console.WriteLine("Enter ID of the staiton\n");
            int.TryParse(Console.ReadLine(), out stationId);

            Console.WriteLine(DalObject.DalObject.GetStation(stationId).ToString());
            return;
        }

        public static void stationListDisplay()
        {
            foreach (var Station in DalObject.DalObject.StationList())
                Console.WriteLine(Station.ToString());    
        }

        public static void droneListDisplay()
        {
            foreach (var Drone in DalObject.DalObject.droneList())           
                Console.WriteLine(Drone.ToString());
            
        }

        public static void customerListDisplay()
        {
            foreach (var Customer in DalObject.DalObject.customerList())            
                Console.WriteLine(Customer.ToString());
            
        }

        public static void parcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.parcelList())            
                Console.WriteLine(Parcel.ToString());
            
        }

        public static void stationsWithFreeSlotsDisplay()
        {
            foreach (var Station in DalObject.DalObject.StationList())            
                if (Station.ChargeSlots != 0)
                    Console.WriteLine(Station.ToString());            
        }

        public static void unassosiatedParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.parcelList())            
                if (Parcel.DroneId != 0)
                    Console.WriteLine(Parcel.ToString());
            
        }
    }
}
