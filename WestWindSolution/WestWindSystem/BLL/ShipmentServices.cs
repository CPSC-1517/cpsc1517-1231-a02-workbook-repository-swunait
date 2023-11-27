using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using WestWindSystem.Paginator;

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

        public List<Shipment> GetBetweenShipmentDate(
            DateTime fromShipmentDate, DateTime toShipDate)
        {
            var query = _westWindContext
                        .Shipments
                        //.Include(s => s.Order)
                        .Include(s => s.ShipViaNavigation)
                        .Include(s => s.Order.Customer)
                        .Where(s => s.ShippedDate >= fromShipmentDate && s.ShippedDate <= toShipDate);
            return query.ToList();
        }

        public Task<PagedResult<Shipment>> GetBetweenShipmentDateWithPaging(
            DateTime fromShipmentDate, DateTime toShipDate,
            int page, int pageSize)
        {
            var query = _westWindContext
                        .Shipments
                        //.Include(s => s.Order)
                        .Include(s => s.ShipViaNavigation)
                        .Include(s => s.Order.Customer)
                        .Where(s => s.ShippedDate >= fromShipmentDate && s.ShippedDate <= toShipDate);
            return Task.FromResult(query
                    .ToPagedResult(page, pageSize));
        }

    }
}
