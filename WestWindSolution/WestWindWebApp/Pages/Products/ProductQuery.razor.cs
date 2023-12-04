using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;   // for CategoryServices, ProductServices
using WestWindSystem.Entities;
using WestWindSystem.Paginator;   // for Category, Product

namespace WestWindWebApp.Pages.Products
{
    public partial class ProductQuery
    {
        #region Paginator
        // Desire current page size
        private const int PAGE_SIZE = 10;
        // Currente page for the paginator
        protected int CurrentPage { get; set; } = 1;
        // Paginator collection of product search results
        protected PagedResult<Product> ProductsQueryResult { get; set; } = new();

        private async Task OnProductSearch()
        {
            // Clear any previous feedback message
            feedbackMessage = null;

            try
            {
                ProductsQueryResult = await CurrentProductServices.GetByProductNameOrCategoryNameOrSupplierCompanyName(
                    searchValue, CurrentPage, PAGE_SIZE);
                if (ProductsQueryResult.RowCount == 0)
                {
                    feedbackMessage = "No result returned.";
                }
            }
            catch(Exception ex)
            {
                feedbackMessage = $"Error executing query with exception: {GetInnerException(ex).Message}";
            }

            await InvokeAsync(StateHasChanged);
        }
        #endregion

        [Inject]
        protected CategoryServices CurrentCategoryServices { get; set; }

        [Inject]
        protected ProductServices CurrentProductServices { get; set; }

        private List<Category> categories = new(); // For populating the categories select element

        private int selectedCategoryId = 0; // The current seleced category
      
        private List<Product> products = new (); // products for a selected category

        private string? feedbackMessage;

        private string? searchValue;

        protected override void OnInitialized()
        {
            categories = CurrentCategoryServices.GetAll();
        }

        private void OnClickSearchByCategory()
        {
            feedbackMessage = null;
            if (selectedCategoryId == 0)
            {
                products.Clear();
            }
            else
            {
                try
                {
                    products = CurrentProductServices.GetByCategoryId(selectedCategoryId);
                }
                catch (Exception ex)
                {
                    feedbackMessage = $"Error fetch catching cateories with exception: {ex.Message}";
                }
            }
        }

        private void OnClickCancel()
        {
            selectedCategoryId = 0;
            products.Clear();
            ProductsQueryResult = new();

		}

        private void OnCategoryChanged(ChangeEventArgs e)
        {
            feedbackMessage = null;
            selectedCategoryId = int.Parse(e.Value.ToString());
            //if (selectedCategoryId == 0)
            //{
            //    products.Clear();
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
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                feedbackMessage = "You must enter at least one character to serach for.";
                products.Clear();
            }
            else
            {
                try
                {
                    products = CurrentProductServices.GetByProductNameOrCategoryNameOrSupplierCompanyName(searchValue);
                    if (products.Count == 0)
                    {
                        feedbackMessage = $"There are products with a product name of {searchValue}";
                    }
                }
                catch (Exception ex)
                {
                    feedbackMessage = $"Error fetch products with exception: {ex.Message}";
                }
            }

        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

    }

}
