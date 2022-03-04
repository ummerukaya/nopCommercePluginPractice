using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.Manufacturer.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority => int.MaxValue;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("Misc.Manufacturer.ManufacturerAll", "Manufacturer/All",
               new { controller = "Manufacturer", action = "ManufacturerAll" });
        }
    }
}
