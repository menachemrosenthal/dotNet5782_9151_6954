using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DalFactory
    {
        public static IDal GetDal(string type)
        {
            if (type == "DalObject")
            {
                return DalObject.Instance;
            }
            //if(type == "DalXml")
            //  return new DalXml();
            throw new ArgumentException("no type matches given argument");
        }
    }
}
