using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
    public static class BlFactory
    {
        public static BlApi.IBL GetBl()
        {
            return BO.BL.Instance;
        }
    }
}
