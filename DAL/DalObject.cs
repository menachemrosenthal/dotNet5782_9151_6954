using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        public DalObject(){ DataSource.Config.Initialize(); }

        public void addDrone() 
        {
            int ndr = DataSource.Config.nextDrone;
            DataSource.Drones[ndr] = new Drone();

            Console.WriteLine("Enter Id\n");
            DataSource.Drones[ndr].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter model\n");
            DataSource.Drones[ndr].Model = Console.ReadLine();

            Console.WriteLine("Enter max weight between 0-2\n");
            DataSource.Drones[ndr].MaxWeight = (WeightCategories)int.Parse(Console.ReadLine());

            DataSource.Config.nextDrone++;
        }

        public void addCostumer()
        {
            int ncsr = DataSource.Config.nextCustomer;
            DataSource.Customers[ncsr] = new Customer();

            Console.WriteLine("Enter ID\n");
            DataSource.Customers[ncsr].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter name\n");
            DataSource.Customers[ncsr].Name = Console.ReadLine();

            Console.WriteLine("Enter phone number\n");
            DataSource.Customers[ncsr].Phone = Console.ReadLine();

            Console.WriteLine("Enter longitude\n");
            DataSource.Customers[ncsr].Longitude = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter lattitude\n");
            DataSource.Customers[ncsr].Lattitude = double.Parse(Console.ReadLine());

            DataSource.Config.nextCustomer++;
        }

        public void addParcel()
        {
            int npcl = DataSource.Config.nextParcel;
            DataSource.Parcels[npcl] = new Parcel();

            Console.WriteLine("Enter ID\n");
            DataSource.Parcels[npcl].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter sender ID\n");
            DataSource.Parcels[npcl].Senderid = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter target ID\n");
            DataSource.Parcels[npcl].TargetId = int.Parse(Console.ReadLine());

            Console.WriteLine("Weight between 0-2\n");
            DataSource.Parcels[npcl].Weight = (WeightCategories)int.Parse(Console.ReadLine());

            DataSource.Parcels[npcl].DroneId = 0;

            DateTime currentDate = DateTime.Now;
            DataSource.Parcels[npcl].Requested = currentDate;
        }

    }
}
