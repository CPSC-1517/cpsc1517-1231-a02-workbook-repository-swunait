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
    public class SupplierServices
    {
        private readonly WestWindContext _westWindContext;

        internal SupplierServices(WestWindContext westWindContext)
        {
            _westWindContext = westWindContext;
        }   

        public List<Supplier> GetAll()
        {
            var query = _westWindContext
                        .Suppliers
                        .OrderBy(s => s.CompanyName)
                        .AsNoTracking();
            return query.ToList();
        }

        public Supplier? Get(int supplierId)
        {
            var query = _westWindContext
                        .Suppliers
                        .Where(s => s.SupplierId == supplierId) 
                        .AsNoTracking();
            return query.FirstOrDefault();  
        }
    }
}
