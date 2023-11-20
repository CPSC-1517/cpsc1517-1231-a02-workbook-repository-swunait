using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Pages.Categories
{
    public partial class CategoryCRUD
    {
        [Inject]
        protected CategoryServices CurrentCategoryServices { get; set; }

        //  The currentCategory being added, edited, or to be deleted
        private Category currentCategory = new();

        // Define a string for feedback message
        private string? feedbackMessage;

        // Define a method that get executed in response to request to add or update category
        private void OnClickSave()
        {
            try
            {
                int categoryId = CurrentCategoryServices.AddCategory(currentCategory);
                feedbackMessage = $"Created category with id {categoryId}";
            }
            catch (ArgumentException ex)
            {
                feedbackMessage = ex.Message;
            }
            catch (Exception ex)
            {
                feedbackMessage = $"Create was not successful with exception {ex.Message}";
            }
        }
    }
}
