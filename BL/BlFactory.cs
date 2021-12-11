using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BlFactory
    {
        public static IBL.IBL GetBl()
        {
            return IBL.BO.BL.Instance;
        }
    }
}
