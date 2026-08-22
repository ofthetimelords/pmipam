using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text;
using Vtable.PmIpam.Models;
using YamlDotNet.Serialization.NamingConventions;

namespace Vtable.PmIpam.Views
{
    public class IndexModel : PageModel
    {
        private readonly Variables _variables;
        public Root Root { get; set; }

        public IndexModel(IOptions<Variables> variables)
        {
            this._variables = variables.Value;
        }

        public void OnGet()
        {
            var ds = new YamlDotNet.Serialization.DeserializerBuilder()
                .WithCaseInsensitivePropertyMatching()
                .WithEnumNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
            using (var reader = new StreamReader(this._variables.Hosts))
            {
                var vlans = ds.Deserialize<Vlan[]>(reader);

                // dirty fix for the hosts
                foreach (var vlan in vlans)
                {
                    if (vlan.Sections == null)
                        vlan.Sections = new List<VlanSection>()
                        {
                            new VlanSection {
                                Name = "(undefined)",
                                Hosts = new List<Models.Host>{
                                new NullHost(),
                                new NullHost(),
                                new NullHost()
                                }
                            }
                        };
                    else
                    {
                        foreach (var section in vlan.Sections)
                        {
                            if (section.Hosts == null)
                                section.Hosts = new List<Models.Host>{
                                new NullHost(),
                                new NullHost(),
                                new NullHost()
                                };
                            else if (section.Hosts.Count < 3)
                                for(var i = 0; i < 3 - section.Hosts.Count; i++)
                                    section.Hosts.Add(new NullHost());
                        }
                    }
                }

                this.Root = new Root { Vlans = vlans };
            }
        }

        public string GetDisabled(Models.Host host)
        {
            return host is NullHost ? "disabled" : string.Empty;
        }
    }
}