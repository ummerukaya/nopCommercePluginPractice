using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Vendors;
using Nop.Core.Events;
using Nop.Plugin.Misc.Manufacturer.Helper;
using Nop.Plugin.Misc.Manufacturer.Models;
using Nop.Plugin.Misc.Manufacturer.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Services.Topics;
using Nop.Services.Vendors;
using Nop.Web.Factories;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.Manufacturer.Factories
{
    public class ManufacturerModelFactory : IManufacturerModelFactory
    {
        
        private readonly ILocalizationService _localizationService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IManufacturerModelService _manufacturerModelService;
        private readonly IManufacturerTemplateService _manufacturerTemplateService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
       // private readonly ManufacturerHelper _manufacturerHelper;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly ITopicService _topicService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IVendorService _vendorService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly VendorSettings _vendorSettings;

        public ManufacturerModelFactory(
            ILocalizationService localizationService,
            IManufacturerService manufacturerService,
            IManufacturerTemplateService manufacturerTemplateService,
            IPictureService pictureService,
            IManufacturerModelService manufacturerModelService,
           
            IProductService productService,
            ISpecificationAttributeService specificationAttributeService,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            ITopicService topicService,
            IUrlHelperFactory urlHelperFactory,
            IUrlRecordService urlRecordService,
            IVendorService vendorService,
            IWebHelper webHelper,
            IWorkContext workContext,
            MediaSettings mediaSettings,
            VendorSettings vendorSettings)
        {
           
            _localizationService = localizationService;
            _manufacturerService = manufacturerService;
            _manufacturerTemplateService = manufacturerTemplateService;
            _pictureService = pictureService;
            _specificationAttributeService = specificationAttributeService;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _topicService = topicService;
            _urlHelperFactory = urlHelperFactory;
            _urlRecordService = urlRecordService;
            _vendorService = vendorService;
            _webHelper = webHelper;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _vendorSettings = vendorSettings;
            _productService = productService;
            _manufacturerModelService = manufacturerModelService;
            
        }
        public async Task<List<ManufacturerModel>> PrepareAllManufacturerModel()
        {
            var model = new List<ManufacturerModel>();

            var currentStore = await _storeContext.GetCurrentStoreAsync();
            //var manufacturerName = await _manufacturerModelService.GetAllManufacturerNameByGroupName(groupname);
            var manufacturers = await _manufacturerModelService.GetAllManufacturersAsync(storeId: currentStore.Id);
            foreach (var manufacturer in manufacturers)
            {
                var modelMan = new ManufacturerModel
                {
                    Id = manufacturer.Id,
                    Name = await _localizationService.GetLocalizedAsync(manufacturer, x => x.Name),
                    Description = await _localizationService.GetLocalizedAsync(manufacturer, x => x.Description),
                    MetaKeywords = await _localizationService.GetLocalizedAsync(manufacturer, x => x.MetaKeywords),
                    MetaDescription = await _localizationService.GetLocalizedAsync(manufacturer, x => x.MetaDescription),
                    MetaTitle = await _localizationService.GetLocalizedAsync(manufacturer, x => x.MetaTitle),
                    SeName = await _urlRecordService.GetSeNameAsync(manufacturer),
                };

                //prepare picture model
                var pictureSize = _mediaSettings.ManufacturerThumbPictureSize;
                var manufacturerPictureCacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.ManufacturerPictureModelKey,
                    manufacturer, pictureSize, true, await _workContext.GetWorkingLanguageAsync(),
                    _webHelper.IsCurrentConnectionSecured(), currentStore);
                modelMan.PictureModel = await _staticCacheManager.GetAsync(manufacturerPictureCacheKey, async () =>
                {
                    var picture = await _pictureService.GetPictureByIdAsync(manufacturer.PictureId);
                    string fullSizeImageUrl, imageUrl;

                    (fullSizeImageUrl, picture) = await _pictureService.GetPictureUrlAsync(picture);
                    (imageUrl, _) = await _pictureService.GetPictureUrlAsync(picture, pictureSize);

                    var pictureModel = new PictureModel
                    {
                        FullSizeImageUrl = fullSizeImageUrl,
                        ImageUrl = imageUrl,
                        Title = string.Format(await _localizationService.GetResourceAsync("Media.Manufacturer.ImageLinkTitleFormat"), modelMan.Name),
                        AlternateText = string.Format(await _localizationService.GetResourceAsync("Media.Manufacturer.ImageAlternateTextFormat"), modelMan.Name)
                    };

                    return pictureModel;
                });

              
                modelMan.ProductCount = await _manufacturerModelService.GetManufacturerProductsByManufacturerIdAsync(manufacturer.Id);
                var groupName =manufacturer.Name[0];
                modelMan.GroupName = (groupName>='A' && groupName<='Z') ? groupName : char.ToUpper(groupName);
                modelMan.GroupId = (modelMan.GroupName - 'A') + 1;

                model.Add(modelMan);
            }

            return model;
        }

        public async Task<List<LayeredManufacturerModel>> PrepareLayeredManufacturerModel()
        {
            var manufacturers = await _manufacturerService.GetAllManufacturersAsync();

            foreach (var item in collection)
            {

            }

        }

        public async Task<ManufacturerGroupModel> PrepareManufacturerGroupModel()
        {
            var manufacturerGroup = new ManufacturerGroupModel();

            manufacturerGroup.GroupNames = ManufacturerHelper.GetAllCustomGroupsName();
            manufacturerGroup.GroupIds = ManufacturerHelper.GetAllGroupIds();

            var allgrouplist = await PrepareAllManufacturerModel();
            
            manufacturerGroup.AvailableGroupsName = ManufacturerHelper.GetAvailableGroupsName(allgrouplist);
            
            manufacturerGroup.ManufacturerModel = allgrouplist;

            return manufacturerGroup;
        }
    }
}
