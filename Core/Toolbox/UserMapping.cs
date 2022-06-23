using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rake_Counter.Core.Toolbox
{
    internal class UserMapping : Dictionary<string, double>
    {
        public void Map(string user, double amount)
        {
            if (!this.ContainsKey(user))
            {
                base[user] = amount;
            }
            else
            {
                base[user] += amount;
            }
        }
    }
}
