using WestWindSystem.Entities;  // for Shipment
using WestWindSystem.BLL;
using Microsoft.AspNetCore.Components;
using WestWindSystem.Paginator;   // for ShipmentServices

namespace WestWindWebApp.Pages.Shipments
{
    public partial class ShipmentQuery
    {
        #region Paginator
        // Desired current page size
        private const int PAGE_SIZE = 10;

        //  current page for the paginator
        protected int CurrentPage { get; set; } = 1;

        //paginator collection of customer Search view
        protected PagedResult<Shipment> PaginatorShipmentSearch { get; set; } = new();

        #endregion

        [Inject]
        protected ShipmentServices CurrentShipmentServices { get; set; }

        private List<Shipment> queryResultList = new();

        private DateTime queryFromShipmentDate = new DateTime(2016,7,15);

        private DateTime queryToShipmentDate = new DateTime(2016, 12, 31);

        private string? feedbackMessage;

        public bool HasFeedback => feedbackMessage != null;

        private void OnSearchClick()
        {
            // Clear any previous feedback message
            feedbackMessage = null;
            PaginatorShipmentSearch = new();

            try
            {
                queryResultList = CurrentShipmentServices.GetBetweenShipmentDate(queryFromShipmentDate, queryToShipmentDate);
                if (queryResultList.Count == 0)
                {
                    feedbackMessage = "No result returned.";
                }
            }
            catch (Exception ex)
            {
                //queryResultList.Clear();

                feedbackMessage = $"Error executing query with exception: {ex.Message}";
            }
        }

        private async Task OnSearchClickWithPaging()
        {
            // Clear any previous feedback message
            feedbackMessage = null;
            queryResultList = new();

            try
            {
                PaginatorShipmentSearch = await CurrentShipmentServices.GetBetweenShipmentDateWithPaging(
                    queryFromShipmentDate, queryToShipmentDate,
                    CurrentPage, PAGE_SIZE);
                if (PaginatorShipmentSearch.RowCount == 0)
                {
                    feedbackMessage = "No result returned.";
                }
            } 
            catch (Exception ex)
            {
                //queryResultList.Clear();
  
                feedbackMessage = $"Error executing query with exception: {ex.Message}";
            }

            await InvokeAsync(StateHasChanged);
        }


    }
}
