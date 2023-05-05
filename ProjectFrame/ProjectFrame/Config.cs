using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameV2
{
    public class Config
    {
        public static string webhook { get; set; }
        public static string platform { get; set; }
        public static bool print { get; set; }
        public static bool usewebhook { get; set; }
    }
}
