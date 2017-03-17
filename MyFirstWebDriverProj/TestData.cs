using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebDriverProj
{
    class TestData
    {
        static object[] IntimeCalcDataNegative =
        {
            new object[] { "Киев", "Харьков", "31", "86", "86", "86", "80" },
            new object[] { "Киев", "Харьков", "2", "86", "86", "86", "80" },
            new object[] { "Киев", "Харьков", "30", "32", "32", "32", "386" },
            new object[] { "Киев", "Харьков", "29", "32", "32", "32", "386" },
        };
    }
}
