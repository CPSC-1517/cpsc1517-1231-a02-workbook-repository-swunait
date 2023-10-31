using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        private WestWindContext _westWindContext;

        internal ProductServices(WestWindContext westWindContext) 
        { 
            _westWindContext = westWindContext;
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _westWindContext
                    .Products
                    .Where(p =>  p.CategoryId == categoryId)
                    .ToList();
        }

        public List<Product> GetByProductName(string partialProductName)
        {
            return _westWindContext
                .Products
                .Where(p => p.ProductName.Contains(partialProductName))
                .ToList();
        }

    }
}
