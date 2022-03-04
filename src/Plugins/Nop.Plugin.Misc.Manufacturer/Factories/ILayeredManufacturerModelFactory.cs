using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Misc.Manufacturer.Models;

namespace Nop.Plugin.Misc.Manufacturer.Factories
{
    public interface ILayeredManufacturerModelFactory
    {
        Task<List<ManufacturerModel>> PrepareAllManufacturerModel();
        Task<ManufacturerGroupModel> PrepareManufacturerGroupModel();

        Task<List<LayeredManufacturerModel>> PrepareLayeredManufacturerModel();
    }
}
