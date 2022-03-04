using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Plugin.Misc.Manufacturer.Services
{
    public class LayeredManufacturerModelService : ILayeredManufacturerModelService
    {
        private readonly IRepository<ProductManufacturer> _productManufacturerRepository;
        private readonly IWorkContext _workContext;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IRepository<Nop.Core.Domain.Catalog.Manufacturer> _manufacturerRepository;
        public LayeredManufacturerModelService(IRepository<ProductManufacturer> productManufacturerRepository,
            IWorkContext workContext,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IRepository<Nop.Core.Domain.Catalog.Manufacturer> manufacturerRepository)
        {
            _productManufacturerRepository = productManufacturerRepository;
            _workContext = workContext;
            _aclService = aclService;
            _storeMappingService = storeMappingService;
            _manufacturerRepository = manufacturerRepository;

        }

      

        public async Task<int> GetManufacturerProductsByManufacturerIdAsync(int id)
        {
            var query = _productManufacturerRepository.Table.Where(x => x.ManufacturerId == id).ToList().Count;
            return await Task.FromResult(query);
        }

        public virtual async Task<IPagedList<Nop.Core.Domain.Catalog.Manufacturer>> GetAllManufacturersAsync(string manufacturerName = "",
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null)
        {
            return await _manufacturerRepository.GetAllPagedAsync(async query =>
            {
                if (!showHidden)
                    query = query.Where(m => m.Published);
                else if (overridePublished.HasValue)
                    query = query.Where(m => m.Published == overridePublished.Value);

                //apply store mapping constraints
                query = await _storeMappingService.ApplyStoreMapping(query, storeId);

                //apply ACL constraints
                if (!showHidden)
                {
                    var customer = await _workContext.GetCurrentCustomerAsync();
                    query = await _aclService.ApplyAcl(query, customer);
                }

                query = query.Where(m => !m.Deleted);

                if (!string.IsNullOrWhiteSpace(manufacturerName))
                    query = query.Where(m => m.Name.Contains(manufacturerName));

                return query.OrderBy(m => m.DisplayOrder).ThenBy(m => m.Id);
            }, pageIndex, pageSize);
        }

        public Task<List<string>> GetAllManufacturerNameByGroupName(char groupName)
        {
            var list = new List<string>();
            if (groupName != '#')
            {
                var query = from m in _manufacturerRepository.Table
                            where m.Name[0] == groupName
                            select m.Name;
                list = (List<string>)query;
            }
            return Task.FromResult(list);
        }
    }
}
