using System.Drawing;
using System.Net;

namespace Vtable.PmIpam.Models
{
    public class Vlan
    {
        public string Name { get; set; }
        public int VlanId {  get; set; }
        public IPNetwork Cidr { get; set; }
        public IList<string> Domains { get; set; } = new List<string>();
        public Color BaseColor { get; set; }
        public Color EndColor { get; set; }
        public string Notes { get; set; }
        public IList<VlanSection> Sections { get; set; } = new List<VlanSection>();
    }
}
