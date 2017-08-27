using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Network
{
    [Serializable]
    public class VersionExtended
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }

        public VersionExtended() { }
        public VersionExtended(int major, int minor, int build)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
        }
    }
}
