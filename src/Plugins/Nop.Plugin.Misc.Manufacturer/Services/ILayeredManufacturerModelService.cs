using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Plugin.Misc.Manufacturer.Services
{
    public interface ILayeredManufacturerModelService
    {
        Task<int> GetManufacturerProductsByManufacturerIdAsync(int id);
        Task<IPagedList<Nop.Core.Domain.Catalog.Manufacturer>> GetAllManufacturersAsync(string manufacturerName = "",
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null);
        Task<List<string>> GetAllManufacturerNameByGroupName(char groupName);
    }
}
