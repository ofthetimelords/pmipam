using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Drawing;
using System.Text;
using Vtable.PmIpam.Models;
using Vtable.PmIpam.ViewModels;
using YamlDotNet.Serialization.NamingConventions;
using static System.Net.Mime.MediaTypeNames;

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
                    if (vlan.Domains == null)
                        vlan.Domains = new List<string>();

                    if (vlan.Sections == null)
                        vlan.Sections = new List<VlanSection>()
                        {
                            new VlanSection {
                                Name = "(undefined)",
                                Hosts = new List<Models.Host>{
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
                                section.Hosts = new List<Models.Host>();

                            if (section.Hosts.Count < 2)
                                for(var i = 0; i < 3 - section.Hosts.Count; i++)
                                    section.Hosts.Add(new NullHost());
                        }
                    }
                }

                this.Root = new Root { Vlans = vlans };
            }
        }


        public HtmlString GetForeColor(Color backColor)
        {
            return backColor.GetBrightness() > 0.4F ? new HtmlString("color: #000;") : HtmlString.Empty;
        }
    }
}