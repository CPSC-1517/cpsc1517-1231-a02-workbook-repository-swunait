using Microsoft.AspNetCore.Components;

namespace NHLBlazorApp.Pages.SamplePages
{
    public partial class UserSignUp
    {

        // Inject an IWebHostEnvironment
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } 

        // Define fields for form field inputs
        private string username = string.Empty;
        private string password = string.Empty;
        private string confirmPassword = string.Empty;

        // Define a field for a single feedback message
        private string? feedbackMessage;
        // Define a field for a collection of error messages
        private List<string> errorMessages = new();


        // Define a method that executes in response to a click event
        private void OnSignUpClick()
        {
            // Clear any previous feedbackMessage or errorMessages
            feedbackMessage = null;
            errorMessages.Clear();

            // Validate that username, password, and confirmPassword are not blank
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessages.Add("Username is required.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessages.Add("Password is required.");
            }
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                errorMessages.Add("Confirm Password is required.");
            }

            // If there are no validation errors then
            // save the username and password the text file users.csv 
            if (errorMessages.Count == 0)
            {
                feedbackMessage = $"Sign Up was successful.";

                // Append to the end of the file users.csv a new line
                // containing: username,password
                string contentRootPath = WebHostEnvironment.ContentRootPath;
                string csvFilePath = $"{contentRootPath}/Data/users.csv";
                try
                {
                    using (StreamWriter writer = new StreamWriter(csvFilePath, true))
                    {
                        writer.WriteLine($"{username},{password}");
                    }
                } 
                catch (Exception ex)
                {
                    feedbackMessage = ex.Message;   
                }
                // Clear the form fields for username, password, confirmPassword
                username = string.Empty;
                password = string.Empty;
                confirmPassword = string.Empty;

                //StateHasChanged();
            }

        }
    }
}
