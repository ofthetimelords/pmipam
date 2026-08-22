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
        public Status? Status { get; set; }
        public Status? Dhcp { get; set; }
        public Status? Metrics { get; set; }
        public string Notes { get; set; }
        public string Todo { get; set; }
    }


    public class NullHost : Host { }
}
