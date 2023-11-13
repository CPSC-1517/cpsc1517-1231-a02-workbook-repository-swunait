using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;   // for CategoryServices, ProductServices
using WestWindSystem.Entities;   // for Category, Product

namespace WestWindWebApp.Pages.Products
{
    public partial class ProductQuery
    {
        [Inject]
        protected CategoryServices CurrentCategoryServices { get; set; }

        private List<Category> categories = new ();

        protected override void OnInitialized()
        {
            categories = CurrentCategoryServices.GetAll();
        }

        private void CategoryChanged()
        {

        }
    }
}
