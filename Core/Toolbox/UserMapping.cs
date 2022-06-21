using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rake_Counter.Core.Toolbox
{
    internal class UserMapping : Dictionary<string, float>
    {
        public void Map(string user, float amount)
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
