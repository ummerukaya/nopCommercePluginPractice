using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.Manufacturer.Models
{
    public record ManufacturerModel : BaseNopEntityModel
    {
        public ManufacturerModel()
        {
            PictureModel = new PictureModel();
            FeaturedProducts = new List<ProductOverviewModel>();
            CatalogProductsModel = new CatalogProductsModel();
        }

        public string Name { get; set; }

        public string SeName { get; set; }
        public char GroupName { get; set; }
        public int GroupId { get; set; }

        public PictureModel PictureModel { get; set; }

        public IList<ProductOverviewModel> FeaturedProducts { get; set; }

        public CatalogProductsModel CatalogProductsModel { get; set; }
        public int ProductCount { get; set; }
        public string Description { get; internal set; }
        public string MetaKeywords { get; internal set; }
        public string MetaDescription { get; internal set; }
        public string MetaTitle { get; internal set; }
    }
}
