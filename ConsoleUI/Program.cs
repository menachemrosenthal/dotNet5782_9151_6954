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

                }

                Console.WriteLine("\nWhat is your next choice?");
                choice = (Choice)int.Parse(Console.ReadLine());
            }
        }





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

        public static void SetParcelToDrone()
        {
            Console.WriteLine("ENTER parcel ID\n");
            int pid = int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER drone ID\n");
            int did = int.Parse(Console.ReadLine());
            DalObject.DalObject.parcelToDrone(pid, did);
        }

        public static void parcelPickup()
        {
           // Console.WriteLine("Enter time of pickup\n");
            //DateTime pdt = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter ID of parcel\n");
            int pid = int.Parse(Console.ReadLine());
            DalObject.DalObject.UpdatePickup(pid);

        }

        public static void parcelDelivered()
        {
           // Console.WriteLine("Enter time of delivery\n");
            //DateTime ddt = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter id of parcel\n");
            int id = int.Parse(Console.ReadLine());
            DalObject.DalObject.UpdateDelivery(id);
        }

        private static void finishCharge()
        {
            Console.WriteLine("ENTER id of drone\n");
            int did = int.Parse(Console.ReadLine());
            DalObject.DalObject.EndCharge(did);
        }

        public static void ParcelDisplay()
        {
            Console.WriteLine("Enter ID of the Parcel\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine("ID: ", DalObject.DalObject.GetParcel(Id).ToString());
            return;
        }

        public static void droneCharge()
        {
            Console.WriteLine("ENTER id of drone\n");
            int did = int.Parse(Console.ReadLine());
            Console.WriteLine("pick a station to charge and enter station id\n");
            stationsWithFreeSlotsDisplay();
            int sid = int.Parse(Console.ReadLine());
            DalObject.DalObject.chargeDrone(did, sid);
        }

        public static void customerDisplay()
        {
            Console.WriteLine("Enter ID of the costumer\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine(DalObject.DalObject.GetCostumer(Id).ToString());
            return;
        }

        public static void DroneDisplay()
        {
            Console.WriteLine("Enter ID of the drone\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine(DalObject.DalObject.GetDrone(Id).ToString());
            return;
        }

        public static void stationDisplay()
        {
            Console.WriteLine("Enter ID of the staiton\n");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine(DalObject.DalObject.GetStation(Id).ToString());
            return;
        }
        
        public static void stationListDisplay()
        {
            foreach (var Station in DalObject.DalObject.stationList())
            {
                Console.WriteLine(Station.ToString());
            }
        }

        public static void droneListDisplay()
        {
            foreach (var Drone in DalObject.DalObject.droneList())
            {
                Console.WriteLine(Drone.ToString());
            }
        }

        public static void customerListDisplay()
        {
            foreach (var Customer in DalObject.DalObject.customerList())
            {
                Console.WriteLine(Customer.ToString());
            }
        }

        public static void parcelListDisplay()
        {
            foreach (var Parcel in DalObject.DalObject.parcelList())
            {
                Console.WriteLine(Parcel.ToString());
            }
        }

        public static void stationsWithFreeSlotsDisplay()
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
}
