using Microsoft.AspNetCore.Components; // for Parameter
using WestWindSystem.BLL;       // for ProductServices
using WestWindSystem.Entities;  // for Product

namespace WestWindWebApp.Pages.Products
{
    public partial class ProductsByCategory
    {
        [Parameter]
        public int? CategoryId { get; set; }

        [Inject]
        protected ProductServices CurrentProductServices { get; set; } 

        [Inject]
        protected NavigationManager CurrentNavigationManager { get; set; }

        private List<Product> products = new();

        //private string? feedbackMessage;

        protected override void OnInitialized()
        {
            //if (CategoryId != null)
            if (CategoryId.HasValue)
            {
                products = CurrentProductServices.GetByCategoryId(CategoryId.Value);
            }
            else
            {
                //feedbackMessage = "CategoryID route parameter is missing.";
                CurrentNavigationManager.NavigateTo("/categories");
            }
        }
    }
}
