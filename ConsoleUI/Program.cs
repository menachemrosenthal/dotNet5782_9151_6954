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
            Console.WriteLine("Welcome to Drone Deliveries!\n" +
                "Pick one of the following options:\n\n" +
                "Add 1-4:\n " +
                "Station-1, Drone-2, Customer-3, Parcel-4.\n\n" +
                "Update 5-9:\n" +
                "Parcel to drone-5, Parcel pickup-6, Delivery-7, Drone charge-8 ,Finish charge-9.\n\n" +
                "Display 10-13:\n" +
                "Station-10, Drone-11, Cistomer-12, Parcel-13.\n\n" +
                "Display Lists 14-19:\n" +
                "Station list-14, Drone list-15, Customer list-16, Parcel list-17," +
                "Unassosiated parcels-18, Stations with free charge slots-19.\n" +
                "END-20 ");

            Choice choice = (Choice)int.Parse(Console.ReadLine()); ;

            while (choice != Choice.end)
            {
                

                switch (choice)
                {
                    case Choice.addStation:
                        Addstation();
                        break;
                    case Choice.addDrone:
                        Adddrone();
                        break;
                    case Choice.addCustomer:
                        Addcustomer();
                        break;
                    case Choice.addParcel:
                        Addparcel();
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

                }

                Console.WriteLine("\nWhat is your next choice?");
                choice = (Choice)int.Parse(Console.ReadLine());
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
            DalObject.DalObject.Addstation(st);
        }
        /// <summary>
        /// addsa drone
        /// </summary>
        public static void Adddrone()
        {
            Drone dr = new();
            Console.WriteLine("ENTER Id\n");
            dr.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER Model\n");
            dr.Model = Console.ReadLine();
            Console.WriteLine("ENTER Battery\n");
            dr.Battery = double.Parse(Console.ReadLine());
            Console.WriteLine("ENTER MaxWeight\n");
            dr.MaxWeight = (IDAL.DO.WeightCategories)int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER Status\n");
            dr.Status = (IDAL.DO.DroneStatuses)int.Parse(Console.ReadLine());
            DalObject.DalObject.AddDrone(dr);

        }
        /// <summary>
        /// adds a customer
        /// </summary>
        public static void Addcustomer()
        {
            Customer cu = new();
            Console.WriteLine("ENTER Id\n");
            cu.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER Name\n");
            cu.Name = Console.ReadLine();
            Console.WriteLine("ENTER phone\n");
            cu.Phone = Console.ReadLine();
            Console.WriteLine("ENTER longitude\n");
            cu.Longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("ENTER latitude\n");
            cu.Lattitude = double.Parse(Console.ReadLine());
            DalObject.DalObject.Addcustumer(cu);

        }
        /// <summary>
        /// adds a parcel
        /// </summary>
        public static void Addparcel()
        {
            Parcel pa = new();
            Console.WriteLine("ENTER sender ID\n");
            pa.Senderid = int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER Target ID\n");
            pa.TargetId = int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER Weight\n");
            pa.Weight = (IDAL.DO.WeightCategories)int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER proirity\n");
            pa.Priority = (IDAL.DO.Priorities)int.Parse(Console.ReadLine());
            pa.DroneId = 0;
            pa.Requested = DateTime.Now;
            DalObject.DalObject.Addparcel(pa);
        }
        /// <summary>
        /// apply a parcel to a drone
        /// </summary>
        public static void SetParcelToDrone()
        {
            Console.WriteLine("ENTER parcel ID\n");
            int pid = int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER drone ID\n");
            int did = int.Parse(Console.ReadLine());
            DalObject.DalObject.ParcelToDrone(pid, did);
        }
        /// <summary>
        /// updates the time of pickup
        /// </summary>
        public static void ParcelPickup()
        {
           // Console.WriteLine("Enter time of pickup\n");
            //DateTime pdt = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter ID of parcel\n");
            int pid = int.Parse(Console.ReadLine());
            DalObject.DalObject.UpdatePickup(pid);

        }
        /// <summary>
        /// updates time of delivery
        /// </summary>
        public static void ParcelDelivered()
        {
           // Console.WriteLine("Enter time of delivery\n");
            //DateTime ddt = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter id of parcel\n");
            int id = int.Parse(Console.ReadLine());
            DalObject.DalObject.UpdateDelivery(id);
        }
        /// <summary>
        /// drone finished charging, updates status 
        /// </summary>
        private static void FinishCharge()
        {
            Console.WriteLine("ENTER id of drone\n");
            int did = int.Parse(Console.ReadLine());
            DalObject.DalObject.EndCharge(did);
        }
        /// <summary>
        /// display parcel
        /// </summary>
        public static void ParcelDisplay()
        {
            Console.WriteLine("Enter ID of the Parcel\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine("ID: ", DalObject.DalObject.GetParcel(Id).ToString());
            return;
        }
        /// <summary>
        /// charging a drone, updates status to charging
        /// </summary>
        public static void DroneCharge()
        {
            Console.WriteLine("ENTER id of drone\n");
            int did = int.Parse(Console.ReadLine());
            Console.WriteLine("pick a station to charge and enter station id\n");
            StationsWithFreeSlotsDisplay();
            int sid = int.Parse(Console.ReadLine());
            DalObject.DalObject.ChargeDrone(did, sid);
        }
        /// <summary>
        /// display customer
        /// </summary>
        public static void CustomerDisplay()
        {
            Console.WriteLine("Enter ID of the costumer\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine(DalObject.DalObject.GetCostumer(Id).ToString());
            return;
        }
        /// <summary>
        /// dispay drone
        /// </summary>
        public static void DroneDisplay()
        {
            Console.WriteLine("Enter ID of the drone\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine(DalObject.DalObject.GetDrone(Id).ToString());
            return;
        }
        /// <summary>
        /// display station
        /// </summary>
        public static void StationDisplay()
        {
            Console.WriteLine("Enter ID of the staiton\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine(DalObject.DalObject.GetStation(Id).ToString());
            return;
        }
        /// <summary>
        /// display the list of stations
        /// </summary>
        public static void StationListDisplay()
        {
            foreach (var Station in DalObject.DalObject.StationList())
            {
                Console.WriteLine(Station.ToString());
            }
        }
        /// <summary>
        /// display the list of drones
        /// </summary>
        public static void DroneListDisplay()
        {
            foreach (var Drone in DalObject.DalObject.DroneList())
            {
                Console.WriteLine(Drone.ToString());
            }
        }
        /// <summary>
        /// display the list of customers
        /// </summary>
        public static void CustomerListDisplay()
        {
            foreach (var Customer in DalObject.DalObject.CustomerList())
            {
                Console.WriteLine(Customer.ToString());
            }
        }
        /// <summary>
        /// display the list of parcels
        /// </summary>
        public static void ParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.ParcelList())
            {
                Console.WriteLine(Parcel.ToString());
            }
        }
        /// <summary>
        /// display list of stations with free charge slots
        /// </summary>
        public static void StationsWithFreeSlotsDisplay()
        {
            foreach (var Station in DalObject.DalObject.StationList())
            {
                if(Station.ChargeSlots != 0)
                     Console.WriteLine(Station.ToString());
            }
        }
       /// <summary>
       /// display list pf unassosiated parcels
       /// </summary>
        public static void UnassosiatedParcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.ParcelList())
            {
                if(Parcel.DroneId != 0)
                    Console.WriteLine(Parcel.ToString());
            }
        }
    }
}
