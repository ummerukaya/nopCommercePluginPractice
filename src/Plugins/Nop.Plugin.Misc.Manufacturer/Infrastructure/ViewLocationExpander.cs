﻿using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.Manufacturer.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "nop.themename";
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] {
                    $"/Plugins/Misc.Manufacturer/Areas/Admin/Views/{{1}}/{{0}}.cshtml",
                    $"/Plugins/Misc.Manufacturer/Areas/Admin/Views/Shared/{{0}}.cshtml"
                }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] {
                    $"/Plugins/Misc.Manufacturer/Views/{{1}}/{{0}}.cshtml",
                    $"/Plugins/Misc.Manufacturer/Views/Shared/{{0}}.cshtml"
                }.Concat(viewLocations);

                if (context.Values.TryGetValue(THEME_KEY, out string theme))
                {
                    viewLocations = new[] {
                        $"/Plugins/Misc.Manufacturer/Themes/{theme}/Views/{{1}}/{{0}}.cshtml",
                        $"/Plugins/Misc.Manufacturer/Themes/{theme}/Views/Shared/{{0}}.cshtml"
                    }.Concat(viewLocations);
                }
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
