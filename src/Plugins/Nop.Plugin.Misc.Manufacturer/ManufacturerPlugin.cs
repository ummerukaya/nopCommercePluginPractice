using System;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Stores;

namespace Nop.Plugin.Misc.Manufacturer
{
    public class ManufacturerPlugin : BasePlugin, IMiscPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly IWebHelper _webHelper;
        private readonly WidgetSettings _widgetSettings;

        public ManufacturerPlugin(IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IStoreService storeService,
            IWebHelper webHelper,
            WidgetSettings widgetSettings)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _storeService = storeService;
            _webHelper = webHelper;
            _widgetSettings = widgetSettings;
        }


        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/Manufacturer/Configure";
        }

        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }


        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
    }
}
