using WestWindSystem.Entities; // for Product, Category, Supplier
using WestWindSystem.BLL; // for ProductServices, CategoryServices, SupplierServices
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WestWindWebApp.Pages.Products
{
    public partial class ProductCRUD
    {
        #region fields
        private Product currentProduct = new();
        private List<Category> categories = new();
        private List<Supplier> suppliers = new();
       
        private string? feedbackMessage;
        private List<string> errors = new();

        #endregion

        #region properties


        [Inject]
        protected ProductServices CurrentProductServices { get; set; }

        [Inject]
        protected CategoryServices CurrentCategoryServices { get; set; }

        [Inject]
        protected SupplierServices CurrentSupplierSevice { get; set; }

        [Inject]
        protected IJSRuntime CurrentJSRuntime { get; set; } // Required to execute javascript

        [Inject]
        protected NavigationManager CurrentNavigationManager { get; set; }

        [Parameter]
        public int? ProductId { get; set; }

        private bool hasErrors => errors.Any();

        #endregion

        #region methods

        protected override void OnInitialized()
        {
            if (ProductId != null)
            {
                currentProduct = CurrentProductServices.GetById(ProductId ?? 0);
            }
            categories = CurrentCategoryServices.GetAll();
            suppliers = CurrentSupplierSevice.GetAll();
        }

        private void OnClickSave()
        {
            errors.Clear();
            try
            {
                if(currentProduct.CategoryId == 0)
                {
                    errors.Add("Please select a Category.");
                }
                if(currentProduct.SupplierId == 0)
                {
                    errors.Add("Please select a Supplier.");
                }
                if(currentProduct.UnitsOnOrder < 0)
                {
                    errors.Add("Units On Order must be zero or more.");
                }
                if(currentProduct.MinimumOrderQuantity < 1)
                {
                    errors.Add("Minimum Order Quantity must be one or more.");
                }
                if (string.IsNullOrWhiteSpace(currentProduct.ProductName))
                {
                    errors.Add("Product Name is required.");
                }

                if (errors.Count == 0)
                {
                    if (currentProduct.ProductId == 0)
                    {
                        int productId = CurrentProductServices.Add(currentProduct);
                        feedbackMessage = $"Successfully created product with productId {productId}";
                    }
                    else
                    {
                        CurrentProductServices.Update(currentProduct);
                        feedbackMessage = "Successfully updated product.";
                    }
                }
                
            }
            catch (Exception ex)
            {
                feedbackMessage = GetInnerException(ex).Message;
            }
        }

        private async void OnClickDelete()
        {
            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm", "Are you sure you want to delete this item?"))
            {
                try
                {
                    CurrentProductServices.Delete(currentProduct);
                    currentProduct = new();
                    feedbackMessage = "Delete was successful";
                    CurrentNavigationManager.NavigateTo("/crud/productcrud");

                }
                catch (Exception ex)
                {
                    feedbackMessage = $"Delete failed with exception {GetInnerException(ex).Message}";
                }
                await InvokeAsync(StateHasChanged);
            }
        }

        private async void OnClickCancel()
        {
            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm", "Are you sure you want to cancel and lose unsaved data?"))
            {
                currentProduct = new();

                await InvokeAsync(StateHasChanged);
            }
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
        #endregion
    }
}
