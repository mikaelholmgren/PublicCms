using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models
{
    public class SettingsBase
    {
        public Guid Id { get; set; }
        public SettingsTypes Type { get; set; }

    }
    public enum SettingsTypes
    {
        Site,
        Page
    }
}
