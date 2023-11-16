using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;   // for CategoryServices, ProductServices
using WestWindSystem.Entities;   // for Category, Product

namespace WestWindWebApp.Pages.Products
{
    public partial class ProductQuery
    {
        //[Inject]
        //protected CategoryServices CurrentCategoryServices { get; set; }

        [Inject]
        protected ProductServices CurrentProductServices { get; set; }

        //private List<Category> categories = new (); // For populating the categories select element

        private int selectedCategoryId = 0; // The current seleced category
      
        private List<Product> products = new (); // products for a selected category

        private string? feedbackMessage;

        private string? productName;

        protected override void OnInitialized()
        {
            //categories = CurrentCategoryServices.GetAll();
        }

        private void OnCategoryChanged(ChangeEventArgs e)
        {
            //feedbackMessage = null;
            //selectedCategoryId = int.Parse(e.Value.ToString());
            //if (selectedCategoryId == 0)
            //{
            //    products.Clear ();
            //}
            //else
            //{
            //    try
            //    {
            //        products = CurrentProductServices.GetByCategoryId(selectedCategoryId);
            //    }
            //    catch (Exception ex)
            //    {
            //        feedbackMessage = $"Error fetch catching cateories with exception: {ex.Message}";
            //    }
            //}

        }

        private void OnProductNameSearch()
        {
            feedbackMessage = null;
            if (string.IsNullOrWhiteSpace(productName))
            {
                feedbackMessage = "You must enter at least one character to serach for.";
                products.Clear();
            }
            else
            {
                try
                {
                    products = CurrentProductServices.GetByProductNameOrCategoryNameOrSupplierCompanyName(productName);
                    if (products.Count == 0)
                    {
                        feedbackMessage = $"There are products with a product name of {productName}";
                    }
                }
                catch (Exception ex)
                {
                    feedbackMessage = $"Error fetch products with exception: {ex.Message}";
                }
            }

        }
    }
}
