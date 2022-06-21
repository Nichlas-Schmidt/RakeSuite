using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rake_Counter.MVVM.Model
{

    internal class Player
    {
        public float Amount { get; set; }

        public float FGAmount { get; set; }

        public string Username { get; set; }


        public Player(float amount, string username, float ratio)
        {
            Amount = amount;
            Username = username;
            FGAmount = amount / ratio;
        }
    }
}
