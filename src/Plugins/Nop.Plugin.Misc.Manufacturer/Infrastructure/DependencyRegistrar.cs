using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Misc.Manufacturer.Factories;
using Nop.Plugin.Misc.Manufacturer.Services;

namespace Nop.Plugin.Misc.Manufacturer.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 3;

        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<ILayeredManufacturerModelFactory, LayeredManufacturerModelFactory>();
            services.AddScoped<ILayeredManufacturerModelService, LayeredManufacturerModelService>();
        }
    }
}
