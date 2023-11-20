using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Pages.Categories
{
    public partial class CategoryCRUDComponent
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
                if (currentCategory.CategoryId == 0)
                {
                    int categoryId = CurrentCategoryServices.AddCategory(currentCategory);
                    feedbackMessage = $"Created category with id {categoryId}";
                }
                else
                {
                    int rowsUpdated = CurrentCategoryServices.UpdateCategory(currentCategory);
                    feedbackMessage = "Successfuly updated category.";
                }
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


        [Inject]
        protected IJSRuntime CurrentJSRuntime { get; set; } // Required to execute javascript

        private async void OnClickDelete()
        {
            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm", "Are you sure you wanto to delete this item?"))
            {
                try
                {
                    CurrentCategoryServices.DeleteCategory(currentCategory.CategoryId);
                    currentCategory = new();
                    feedbackMessage = "Delete was successful";
                }
                catch(Exception ex)
                {
                    feedbackMessage = $"Delete failed with exception {ex.Message}";
                }
                await InvokeAsync(StateHasChanged);
            }
        }

    }
}
