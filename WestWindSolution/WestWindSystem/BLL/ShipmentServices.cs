using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    public class ShipmentServices
    {
        private readonly WestWindContext _westWindContext;

        internal ShipmentServices(WestWindContext westWindContext)
        {
            _westWindContext = westWindContext;
        }

        public List<Shipment> GetByShipmentDate(DateTime shipmentDate)
        {
            var query = _westWindContext
                        .Shipments
                        //.Include(s => s.Order)
                        .Include(s => s.ShipViaNavigation)
                        .Include(s => s.Order.Customer)
                        .Where(s => s.ShippedDate == shipmentDate);
            return query.ToList();
        }

    }
}
