using System;

namespace ConsoleUI
{
    enum Choice {addStation = 1, addDrone, addCustomer, addParcel,
        parcelToDrone, pickup, delivery, droneCharge, finishCharge, 
        stationDisplay, droneDisplay, customerDisplay, parcelDisplay, 
    stationsList, dronesList, customersList, pacelsList, unassosiatedParcelsList, stationsWithFreeSlots,
    end}
    class Program
    {
        static void Main(string[] args)
        {
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                
            }
        }
    }
}
