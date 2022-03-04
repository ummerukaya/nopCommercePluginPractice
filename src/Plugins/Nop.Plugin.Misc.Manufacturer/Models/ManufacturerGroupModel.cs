using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nop.Plugin.Misc.Manufacturer.Models
{
    public class ManufacturerGroupModel
    {
        public ManufacturerGroupModel()
        {
            AllGroupsName = new List<char>();
            AvailableGroupsName = new List<char>();
            ManufacturerModel = new List<ManufacturerModel>();
        }

        public IList<char> AvailableGroupsName { get; set; }
        public IList<char> AllGroupsName { get; set; }
        public List<int> GroupIds { get; set; }
        public List<char> GroupNames { get; set; }
        public int SelectedGroupId { get; set; }
        public IList<ManufacturerModel> ManufacturerModel { get; set; }
    }
}
