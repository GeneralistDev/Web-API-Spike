using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_API_Spike.Configuration
{
    public interface ISettings
    {
        string GetConnectionString(string name);
    }
}
