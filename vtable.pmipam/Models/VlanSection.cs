using System.Net;

namespace Vtable.PmIpam.Models
{
    public class VlanSection
    {
        public string Name { get; set; }

        public IPNetwork Cidr { get; set; }
        public IList<Host> Hosts { get; set; } = new List<Host>();
    }
}
