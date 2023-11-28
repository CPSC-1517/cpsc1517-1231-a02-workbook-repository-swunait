using WestWindSystem.Entities; // for Product, Category, Supplier
using WestWindSystem.BLL; // for ProductServices, CategoryServices, SupplierServices
using Microsoft.AspNetCore.Components;

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

        #endregion

        #region methods

        protected override void OnInitialized()
        {
            categories = CurrentCategoryServices.GetAll();
            suppliers = CurrentSupplierSevice.GetAll();
        }

        private void OnClickSave()
        {
            try
            {
                int productId = CurrentProductServices.Add(currentProduct);
                feedbackMessage = $"Successfully created product with productId {productId}";
            }
            catch (Exception ex)
            {
                feedbackMessage = GetInnerException(ex).Message;
            }
        }

        private void OnClickDelete()
        {

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
