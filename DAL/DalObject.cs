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
            DataSource.drones[ndr] = new Drone();

            Console.WriteLine("Enter Id\n");
            DataSource.drones[ndr].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter model\n");
            DataSource.drones[ndr].Model = Console.ReadLine();

            Console.WriteLine("Enter max weight between 0-2\n");
            DataSource.drones[ndr].MaxWeight = (WeightCategories)int.Parse(Console.ReadLine());

            DataSource.Config.nextDrone++;
        }

        public void addCostumer()
        {
            int ncsr = DataSource.Config.nextCustomer;
            DataSource.customers[ncsr] = new Customer();

            Console.WriteLine("Enter ID\n");
            DataSource.customers[ncsr].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter name\n");
            DataSource.customers[ncsr].Name = Console.ReadLine();

            Console.WriteLine("Enter phone number\n");
            DataSource.customers[ncsr].Phone = Console.ReadLine();

            Console.WriteLine("Enter longitude\n");
            DataSource.customers[ncsr].Longitude = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter lattitude\n");
            DataSource.customers[ncsr].Lattitude = double.Parse(Console.ReadLine());

            DataSource.Config.nextCustomer++;
        }

        public void addParcel()
        {
            int npcl = DataSource.Config.nextParcel;
            DataSource.parcels[npcl] = new Parcel();

            Console.WriteLine("Enter ID\n");
            DataSource.parcels[npcl].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter sender ID\n");
            DataSource.parcels[npcl].Senderid = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter target ID\n");
            DataSource.parcels[npcl].TargetId = int.Parse(Console.ReadLine());

            Console.WriteLine("Weight between 0-2\n");
            DataSource.parcels[npcl].Weight = (WeightCategories)int.Parse(Console.ReadLine());

            DataSource.parcels[npcl].DroneId = 0;

            DateTime currentDate = DateTime.Now;
            DataSource.parcels[npcl].Requested = currentDate;
        }

    }
}
