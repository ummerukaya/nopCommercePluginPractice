using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.Manufacturer.Models
{
    public record LayeredManufacturerModel : BaseNopModel
    {
        public string Group { get; set; }
        public ManufacturerModel ManufacturerModel { get; set; }
        public int ProductCount { get; set; }

    }
}
