using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerInParcel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"\n   Name: {Name}\n   ID: {Id}";
        }
    }
    
}
