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
            stationsList, dronesList, customersList, pacelsList, unassosiatedParcelsList, stationsWithFreeSlots,
            end
        }
        public static void Addstation()
        {
            Station st = new();
            Console.WriteLine("ENTER Id\n");
            st.Id = Console.Read();
            Console.WriteLine("ENTER Name\n");
            st.Name = Console.Read();
            Console.WriteLine("ENTER Charge slots\n");
            st.ChargeSlots = Console.Read();
            Console.WriteLine("ENTER longitude\n");
            st.Longitude = Console.Read();
            Console.WriteLine("ENTER latitude\n");
            st.Lattitude = Console.Read();
            DalObject.DalObject.Addstation(st);
        }
        public static void Adddrone()
        {
            Drone dr = new();
            Console.WriteLine("ENTER Id\n");
            dr.Id = Console.Read();
            Console.WriteLine("ENTER Model\n");
            dr.Model = Console.ReadLine();
            Console.WriteLine("ENTER Battery\n");
            dr.Battery = Console.Read();
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
            cu.Id = Console.Read();
            Console.WriteLine("ENTER Name\n");
            cu.Name = Console.ReadLine();
            Console.WriteLine("ENTER phone\n");
            cu.Phone = Console.ReadLine();
            Console.WriteLine("ENTER longitude\n");
            cu.Longitude = Console.Read();
            Console.WriteLine("ENTER latitude\n");
            cu.Lattitude = Console.Read();
            DalObject.DalObject.Addcustumer(cu);

        }
        public static void Addparcel()
        {
            Parcel pa = new();
            Console.WriteLine("ENTER Id\n");
            pa.Id = Console.Read();
            Console.WriteLine("ENTER sender id\n");
            pa.Senderid = Console.Read();
            Console.WriteLine("ENTER TargetId\n");
            pa.TargetId = Console.Read();
            Console.WriteLine("ENTER Weight\n");
            pa.Weight = (IDAL.DO.WeightCategories)int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER proirity\n");
            pa.Proirity = (IDAL.DO.Priorities)int.Parse(Console.ReadLine());
            Console.WriteLine("ENTER drone Id\n");
            pa.DroneId = Console.Read();
            Console.WriteLine("ENTER Requested tine\n");
            pa.Requested = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("ENTER scheduled time\n");
            pa.Scheduled = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("ENTER picked up time\n");
            pa.PickedUp = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("ENTER Delivered time\n");
            pa.Delivered = DateTime.Parse(Console.ReadLine());

            DalObject.DalObject.Addparcel(pa);
        }
        public static void Setparceltodrone()
        {
            Console.WriteLine("ENTER parcel id\n");
            int pid = Console.Read();
            Console.WriteLine("ENTER drone id\n");
            int did = Console.Read();
            DalObject.DalObject.parceltodrone(pid, did);
        }




        static void Main(string[] args)
        {
            Choice choice = (Choice)int.Parse(Console.ReadLine());

            Console.WriteLine("Welcome to Drone Deliveries!\n" +
                "Pick one of the following options:\n" +
                "Add 1-4:\n " +
                "station-1, drone-2, customer-3, parcel-4." +
                "Update 5-9:\n" +
                "parcel to drone-5, pickup-6, delivery-7, drone charge-8 ,finish charge-9.\n" +
                "Display 10-13:\n" +
                "station-10, drone-11, customer-12, parcel-13.\n" +
                "Display Lists 14-19:\n" +
                "station list-14, drone list-15, customer list-16, parcel list-17," +
                "unassosiated parcels-18, stations with free charge slots-19.\n" +
                "END-20 ");

            switch (choice)
            {
                case Choice.addStation: Addstation();
                    break;
                case Choice.addDrone: Adddrone();
                    break;
                case Choice.addCustomer: Addcustomer();
                    break;
                case Choice.addParcel: Addparcel();
                    break;
                case Choice.stationDisplay: stationDisplay();
                    break;
                case Choice.droneDisplay: DroneDisplay();
                    break;
                case Choice.customerDisplay: customerDisplay();
                    break;
                case Choice.parcelToDrone: Setparceltodrone();                  
                    break;
                case Choice.pickup:
                    {
                        Console.WriteLine("Enter time of pickup\n");
                        DateTime pdt = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter id of parcel\n");
                        int ppid = int.Parse(Console.ReadLine());
                        DalObject.DalObject.Updatepickup(pdt, ppid);
                        break;
                    }
                case Choice.delivery:
                    {
                        Console.WriteLine("Enter time of delivery\n");
                        DateTime ddt = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter id of parcel\n");
                        int id = int.Parse(Console.ReadLine());
                        DalObject.DalObject.Updatedelivery(ddt, id);
                        break;
                    }
                case Choice.droneCharge: 
                    {
                        Console.WriteLine("ENTER id of drone\n");
                        int did = int.Parse(Console.ReadLine());
                        Console.WriteLine("pick a station to charge and enter station id\n");
                        //stationlistdisplay();
                        int sid = int.Parse(Console.ReadLine());
                        DalObject.DalObject.Chargedrone(did, sid);
                        break;
                    }
                case Choice.finishCharge:
                    {
                        Console.WriteLine("ENTER id of drone\n");
                        int did = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the station of charge id\n");
                        //stationlistdisplay();
                        int sid = int.Parse(Console.ReadLine());
                        DalObject.DalObject.Endcharge(did, sid);
                        break;
                    }
                    
            }



        }
       
       
        private static void customerDisplay()
        {
            Console.WriteLine("Enter ID of the costumer\n");
            int Id = int.Parse(Console.ReadLine());

            Console.WriteLine("ID: ", DalObject.DalObject.GetCostumer(Id).Id, "\n");
            Console.WriteLine("Name: ", DalObject.DalObject.GetCostumer(Id).Name, "\n");
            Console.WriteLine("Phone number: ", DalObject.DalObject.GetCostumer(Id).Phone, "\n");
            Console.WriteLine("Longitube: ", DalObject.DalObject.GetCostumer(Id).Longitude, "\n");
            Console.WriteLine("Lattitude: ", DalObject.DalObject.GetCostumer(Id).Lattitude, "\n");

            return;
        }

        private static void DroneDisplay()
        {
            Console.WriteLine("Enter ID of the drone\n");
            int Id = int.Parse(Console.ReadLine());

            Console.WriteLine("ID: ", DalObject.DalObject.GetDrone(Id).Id, "\n");
            Console.WriteLine("Model: ", DalObject.DalObject.GetDrone(Id).Model, "\n");
            Console.WriteLine("Battery: ", DalObject.DalObject.GetDrone(Id).Battery, "\n");
            Console.WriteLine("Max weight: ", DalObject.DalObject.GetDrone(Id).MaxWeight, "\n");
            Console.WriteLine("Status: ", DalObject.DalObject.GetDrone(Id).Status, "\n");

            return;
        }

        private static void stationDisplay()
        {
            Console.WriteLine("Enter ID of the staiton\n");
            int Id = int.Parse(Console.ReadLine());

            Console.WriteLine("ID: ", DalObject.DalObject.GetStation(Id).Id,"\n");
            Console.WriteLine("Name: ", DalObject.DalObject.GetStation(Id).Name, "\n");
            Console.WriteLine("Charge slots: ", DalObject.DalObject.GetStation(Id).ChargeSlots,"\n");
            Console.WriteLine("Longitube: ", DalObject.DalObject.GetStation(Id).Longitude, "\n");
            Console.WriteLine("Lattitude: ", DalObject.DalObject.GetStation(Id).Lattitude, "\n");

            return;
        }
    }



}
