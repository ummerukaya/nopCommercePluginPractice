using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.LoginManager.Controllers;
using Nop.Web.Controllers;

namespace Nop.Plugin.Misc.LoginManager
{
    public class NopStartup : INopStartup
    {
        public int Order => 10;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CustomerController, LoginManagerCustomerController>();
        }
    }
}
