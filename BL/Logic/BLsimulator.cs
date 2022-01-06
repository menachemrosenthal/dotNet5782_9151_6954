using System.Runtime.CompilerServices;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;

namespace BO
{
    internal class Simulator
    {
        public static int speed = 100;
        public static int timer = 1000;

        public Simulator(int droneId, Action update, Func<bool> finish, IBL bl)
        {
            while (finish())
            {
                /////
            }
        }
    }
}
