using System;
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
        static void Main(string[] args)
        {
            Choice choice = (Choice)int.Parse(Console.ReadLine());
            switch (choice)
            {
                case Choice.addStation: 
                    break;
                case Choice.stationDisplay: stationDisplay();
                    break;
                case Choice.droneDisplay: DroneDisplay();
                    break;
                case Choice.customerDisplay: customerDisplay();
                    break;
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
