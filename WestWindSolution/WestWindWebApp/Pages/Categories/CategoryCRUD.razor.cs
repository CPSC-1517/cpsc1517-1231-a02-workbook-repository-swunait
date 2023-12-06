using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Pages.Categories
{
    public partial class CategoryCRUD
    {
        [Parameter]
        public int? CategoryId { get; set; }


        [Inject]
        protected CategoryServices CurrentCategoryServices { get; set; }

        //  The currentCategory being added, edited, or to be deleted
        private Category currentCategory = new();

        // Define a string for feedback message
        private string? feedbackMessage;

        // Define a list of errors
        private List<string> errors = new();

        // Define a method that get executed in response to request to add or update category
        private void OnClickSave()
        {
            feedbackMessage = null;
            errors.Clear();

            if (string.IsNullOrWhiteSpace(currentCategory.CategoryName))
            {
                errors.Add("Category Name is required.");
            }
            if (string.IsNullOrWhiteSpace(currentCategory.Description))
            {
                errors.Add("Description is required.");
            }

            if (errors.Count == 0)
            {
                try
                {
                    if (currentCategory.CategoryId == 0)
                    {
                        int categoryId = CurrentCategoryServices.AddCategory(currentCategory);
                        feedbackMessage = $"Created category with id {categoryId}";
                        CurrentNavigationManager.NavigateTo($"/crud/categorycrud/{categoryId}");
                    }
                    else
                    {
                        int rowsUpdated = CurrentCategoryServices.UpdateCategory(currentCategory);
                        feedbackMessage = "Successfuly updated category.";
                    }
                }
                catch (ArgumentException ex)
                {
                    feedbackMessage = GetInnerException(ex).Message;
                }
                catch (Exception ex)
                {
                    feedbackMessage = $"Create was not successful with exception {GetInnerException(ex).Message}";
                }
            }

            
        }


        [Inject]
        protected IJSRuntime CurrentJSRuntime { get; set; } // Required to execute javascript

        [Inject]
        protected NavigationManager CurrentNavigationManager { get; set; }  

        private async void OnClickDelete()
        {
            feedbackMessage = null;
            errors.Clear();

            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm", "Are you sure you wanto to delete this item?"))
            {
                try
                {
                    CurrentCategoryServices.DeleteCategory(currentCategory.CategoryId);
                    currentCategory = new();
                    feedbackMessage = "Delete was successful";
                    CurrentNavigationManager.NavigateTo("/crud/categorycrud");

                }
                catch(Exception ex)
                {
                    feedbackMessage = $"Delete failed with exception {GetInnerException(ex).Message}";
                }
                await InvokeAsync(StateHasChanged);
            }
        }
        
        private async void OnClickNavigateToCategorySelection()
        {
            feedbackMessage = null;
            errors.Clear();

            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm",
                "Are you sure you want to navigate to category selection and lose all unsaved changes"))
            {
                CurrentCategoryServices.UndoChanges();
                CurrentNavigationManager.NavigateTo("/query/categories");
            }
        }

        private async void OnClickCancelEdit()
        {
            feedbackMessage = null;
            errors.Clear();

            if (await CurrentJSRuntime.InvokeAsync<bool>(
                "confirm",
                "Are you sure you want to clear the form and lose all unsaved changes?"))
            {
                
                currentCategory = new();
                CurrentNavigationManager.NavigateTo("/crud/categorycrud");

                StateHasChanged();
            }
        }

        protected override void OnInitialized()
        {
            // Check if CategoryId parameter has a value
            if (CategoryId.HasValue)
            {
                // Fetch from the database a Category contain the CategoryId value
                currentCategory = CurrentCategoryServices.Get(CategoryId ?? 0);
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
