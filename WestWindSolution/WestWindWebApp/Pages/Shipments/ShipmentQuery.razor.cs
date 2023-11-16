using WestWindSystem.Entities;  // for Shipment
using WestWindSystem.BLL;
using Microsoft.AspNetCore.Components;   // for ShipmentServices

namespace WestWindWebApp.Pages.Shipments
{
    public partial class ShipmentQuery
    {
        [Inject]
        protected ShipmentServices CurrentShipmentServices { get; set; }    

        private List<Shipment> queryResultList = new();

        private DateTime queryShipmentDate = new DateTime(2016,7,15);

        private string? feedbackMessage;

        public Boolean HasFeedback => feedbackMessage != null;

        private void OnSearchClick()
        {
            // Clear any previous feedback message
            feedbackMessage = null;

            try
            {
                queryResultList = CurrentShipmentServices.GetByShipmentDate(queryShipmentDate);
                if (queryResultList.Count == 0)
                {
                    feedbackMessage = "No result returned.";
                }
            } 
            catch (Exception ex)
            {
                queryResultList.Clear();
                feedbackMessage = $"Error executing query with exception: {ex.Message}";
            }

        }


    }
}
