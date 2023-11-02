using WestWindSystem.Entities; // for Category
using WestWindSystem.BLL; // for CategoryServices
using Microsoft.AspNetCore.Components;

namespace WestWindWebApp.Pages.Categories
{
    public partial class CategoryList
    {
        // Inject a CategoryServices BLL property named CurrentCategoryServices
        [Inject]
        protected CategoryServices CurrentCategoryServices { get; set; }

        // Define a list of categories 
        private List<Category> categories = new();

        // Define a field for the cateogry description to search for
        private string categoryDescription;

        private void OnSearchByDescription()
        {
            // Call the FindByDescription of the CategoryServices
            categories = CurrentCategoryServices.FindByDescription(categoryDescription);
        }

        // Fetch from our database use CurrentCategoryServices each time
        // this components loads
        protected override void OnInitialized()
        {
            categories = CurrentCategoryServices.GetAll();
        }
    }
}
