using Vtable.PmIpam.Models;
using Host = Vtable.PmIpam.Models.Host;

namespace Vtable.PmIpam.ViewModels
{
    public class RowTDModel
    {
        public Vlan Vlan { get; set; }
        public Host Host { get; set; }
        public VlanSection Section { get; set; }
        public int VlanTableRow { get; set; }
        public int HostTableRow { get; set; }
        public int SectionTableRow { get; set; }
        public int VlanSectionIndex { get; set; }
        public int TotalHostsInSection { get; set; }
        public int TotalHostsInVlan { get; set; }
        public string VlanRowStyle { get; set; }
        public string RowStyle { get; set; }

        public string GetDisabled(Models.Host host)
        {
            return host is NullHost ? "disabled" : string.Empty;
        }
    }
}
