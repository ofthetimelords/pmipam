using System.Net;
using YamlDotNet.Serialization;

namespace Vtable.PmIpam.Models
{
    public class Host
    {
        public string Name { get; set; }
        public int? HA { get; set; }
        public IPAddress IP { get; set; }
        [YamlMember(Alias = "status")]
        public Status Status { get; set; } = Models.Status.Enabled;
        public Status Dhcp { get; set; } = Models.Status.Enabled;
        public Status Metrics { get; set; } = Models.Status.Enabled;
        public string Notes { get; set; }
        public string Todo { get; set; }
    }


    public class NullHost : Host
    {
        public NullHost()
        {
            this.Status = Models.Status.Null;
            this.Dhcp = Models.Status.Null;
            this.Metrics = Models.Status.Null;
        }
    }
}