using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rake_Counter.MVVM.Model
{

    internal class Player
    {
        public double Amount { get; set; }

        public double FGAmount { get; set; }

        public string Username { get; set; }


        public Player(double amount, string username, double ratio)
        {
            Amount = amount;
            Username = username;
            FGAmount = amount / ratio;
        }
    }
}
