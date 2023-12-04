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
            try
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
            catch (Exception ex)
            {
                feedbackMessage = GetInnerException(ex).Message;
            }
        }

        private async void OnClickDelete()
        {
            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm", "Are you sure you wanto to delete this item?"))
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

        private void OnClickCancel()
        {

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
