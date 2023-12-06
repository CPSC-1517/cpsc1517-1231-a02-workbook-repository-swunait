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
    public class ProductServices
    {
        private readonly WestWindContext _westWindContext;

        internal ProductServices(WestWindContext westWindContext) 
        { 
            _westWindContext = westWindContext;
        }

        private void ValidateProduct(Product existingProduct)
        {
            if (existingProduct == null)
            {
                throw new ArgumentNullException(nameof(existingProduct), "A product is required.");
            }
            // Validate CategoryId of existingProduct exists in the database
            if ( _westWindContext.Categories.Any(c => c.CategoryId == existingProduct.CategoryId) == false)
            {
                throw new ArgumentException($"CategoryId {existingProduct.CategoryId} does not exists.");
            }
            // Validate SupplierId of existingProduct exists in the database
            if ( _westWindContext.Suppliers.Any(s => s.SupplierId == existingProduct.SupplierId) == false)
            {
                throw new ArgumentException($"SupplierId {existingProduct.SupplierId} does not exists.");
            }
            // Validate UnitPrice is more than $1.00
            if (existingProduct.UnitPrice < 1)
            {
                throw new ArgumentException("Product unit price must be at minimum $1.00 or more.");
            }
        }
        public int Add(Product newProduct)
        {
            ValidateProduct(newProduct);


            newProduct.Discontinued = false;
            _westWindContext.Products.Add(newProduct);
            _westWindContext.SaveChanges();
            return newProduct.ProductId;
        }

        public int Update(Product existingProduct) 
        {
            ValidateProduct(existingProduct);

            //_westWindContext.Products.Update(existingProduct);
            // https://learn.microsoft.com/en-us/ef/core/change-tracking/identity-resolution
            // Query then apply changes
            var trackedProduct = _westWindContext.Products.Find(existingProduct.ProductId);
            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.propertyvalues.setvalues?view=efcore-7.0
            // Copy all the values from existingProduct to set them on the trackedProduct 
            _westWindContext.Entry(trackedProduct).CurrentValues.SetValues(existingProduct);

            int rowsUpdated = _westWindContext.SaveChanges();
            return rowsUpdated;
        }

        public int Delete(Product existingProduct) 
        {
            //_westWindContext.Products.Remove(existingProduct); // hard-delete
            //int rowsDeleted = _westWindContext.SaveChanges();
            //return rowsDeleted;

            // Validate incoming parameters are not null
            if (existingProduct == null)
            {
                throw new ArgumentNullException(nameof(existingProduct), "Product is required.");
            }

            // Check if there any OrderDetails that references this product
            bool inAnyOrderDetails = _westWindContext
                .OrderDetails
                .Any(od => od.ProductId == existingProduct.ProductId);
            if (inAnyOrderDetails)
            {
                throw new ArgumentException($"ProductID {existingProduct.ProductId} is being reference from OrderDetails and cannot be deleted. ");
            }

            existingProduct.Discontinued = true;    // soft-delete
            return Update(existingProduct);
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _westWindContext
                    .Products
                    .Where(p => p.Discontinued == false)
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .Where(p =>  p.CategoryId == categoryId && p.Discontinued == false)
                    .AsNoTracking()
                    .ToList();
        }

        public Product? GetById(int productId)
        {
            var query = _westWindContext
                            .Products
                            .Where(p => p.Discontinued == false && p.ProductId == productId)
                            .AsNoTracking();
            return query.FirstOrDefault();               
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
                .Where(p => p.Discontinued == false)
                .Where(p => p.ProductName.Contains(partialName) 
                            || p.Category.CategoryName.Contains(partialName)
                            || p.Supplier.CompanyName.Contains(partialName) 
                            )
                .AsNoTracking()
                .ToList();
        }

        public Task<PagedResult<Product>> GetByProductNameOrCategoryNameOrSupplierCompanyName(
            string partialName,
            int page,
            int pageSize)
        {
            var query = _westWindContext
               .Products
               .Include(p => p.Category)
               .Include(p => p.Supplier)
               .Where(p => p.Discontinued == false)
               .Where(p => p.ProductName.Contains(partialName)
                           || p.Category.CategoryName.Contains(partialName)
                           || p.Supplier.CompanyName.Contains(partialName)
                           )
               .AsNoTracking();
            return Task.FromResult(query.ToPagedResult(page, pageSize));
        }


    }
}
