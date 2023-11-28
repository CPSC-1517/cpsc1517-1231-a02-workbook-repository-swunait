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
    public class ProductServices
    {
        private readonly WestWindContext _westWindContext;

        internal ProductServices(WestWindContext westWindContext) 
        { 
            _westWindContext = westWindContext;
        }

        public int Add(Product newProduct)
        {
            // Validate that newProduct is not null
            if (newProduct == null)
            {
                throw new ArgumentNullException(nameof(newProduct),"A new product is required.");
            }


            newProduct.Discontinued = false;
            _westWindContext.Products.Add(newProduct);
            _westWindContext.SaveChanges();
            return newProduct.ProductId;
        }

        public int Update(Product existingProduct) 
        {
            _westWindContext.Products.Update(existingProduct);
            int rowsUpdated = _westWindContext.SaveChanges();
            return rowsUpdated;
        }

        public int Delete(Product existingProduct) 
        {
            //_westWindContext.Products.Remove(existingProduct); // hard-delete
            //int rowsDeleted = _westWindContext.SaveChanges();
            //return rowsDeleted;

            existingProduct.Discontinued = true;    // soft-delete
            return Update(existingProduct);
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _westWindContext
                    .Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .Where(p =>  p.CategoryId == categoryId)
                    .ToList();
        }

        /// <summary>
        /// Return a list of Product with a matching partial value for
        /// either the ProductName or Category CategoryName or Supplier CompanyName
        /// </summary>
        /// <param name="partialName">The value to search for</param>
        /// <returns>A list of matching products</returns>
        public List<Product> GetByProductNameOrCategoryNameOrSupplierCompanyName(
            string partialName)
        {
            return _westWindContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.ProductName.Contains(partialName) 
                            || p.Category.CategoryName.Contains(partialName)
                            || p.Supplier.CompanyName.Contains(partialName) 
                            )
                .ToList();
        }

    }
}
