using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.Manufacturer.Factories;
using Nop.Plugin.Misc.Manufacturer.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Misc.Manufacturer.Controllers
{
    public class ManufacturerController : BasePluginController
    {
        private readonly IManufacturerModelFactory _manufacturerFactory;
        private readonly IManufacturerModelService _manufacturerService;

        public ManufacturerController(IManufacturerModelFactory manufacturerFactory,
            IManufacturerModelService manufacturerService)
        {
            _manufacturerFactory = manufacturerFactory;
            _manufacturerService = manufacturerService;
        }

        public async Task<IActionResult> ManufacturerGroupAsync()
        {
            var model = await _manufacturerFactory.PrepareManufacturerGroupModel();

            return View(model);
        }

        //public async Task<IActionResult> ManufacturerAll()
        //{
        //    var model = await _manufacturerFactory.PrepareAllManufacturerModel();

        //    return View(model);
        //}
    }
}
