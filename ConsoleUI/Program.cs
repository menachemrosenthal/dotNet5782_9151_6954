using System;
using IDAL.DO;
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
            ////Func(st);
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
            ///func(dr);
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
            ////Func(cu);
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

            ////Func(pa);
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
            }



        }
       
       
    }



}
