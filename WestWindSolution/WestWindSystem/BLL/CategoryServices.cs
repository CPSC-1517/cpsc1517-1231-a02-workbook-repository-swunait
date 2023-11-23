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
    public class CategoryServices
    {
        private readonly WestWindContext _westWindContext;

        internal CategoryServices(WestWindContext westWindContext) 
        {
            _westWindContext = westWindContext;
        }

        public List<Category> GetAll()
        {
            return _westWindContext.Categories.ToList();
        }

        public Category? Get(int categoryId)
        {
            //return _westWindContext
            //        .Categories
            //        .Where(c => c.CategoryId == categoryId)
            //        .FirstOrDefault();
            return _westWindContext.Find<Category>(categoryId);
        }

        public List<Category> FindByDescription(string partialDescription)
        {
            return _westWindContext
                    .Categories
                    .Where(c => c.Description.Contains(partialDescription))
                    .ToList();
        }

        public int AddCategory(Category newCategory)
        {
            // Verify newCategory is not null
            if (newCategory == null)
            {
                throw new ArgumentNullException(nameof(newCategory), "newCategory parameter cannot be null");
            }
            // Verify the category name is unqiue
            bool categoryNameExists = _westWindContext
                                        .Categories         
                                        .Any(c => c.CategoryName == newCategory.CategoryName);
            if (categoryNameExists)
            {
                throw new ArgumentException($"The category name {newCategory.CategoryName} is already in use.");
            }
            // Add the newCategory to the Categories DbSet
            _westWindContext.Categories.Add(newCategory);
            // Save the changes to the database
            _westWindContext.SaveChanges();
            // Return the generate categoryId value
            return newCategory.CategoryId;
        }

        public int UpdateCategory(Category existingCategory)
        {
            //_westWindContext.Categories.Entry(existingCategory).State = EntityState.Modified;
            _westWindContext.Categories.Update(existingCategory);
            int rowsUpdated = _westWindContext.SaveChanges();
            return rowsUpdated;
        }

        public int DeleteCategory(int categoryId)
        {
            // Find an existing Category that matches the categoryId value
            Category? existingCategory = _westWindContext
                                            .Categories
                                            .Where(c => categoryId == c.CategoryId)
                                            .FirstOrDefault();
            // Throw an ArgumentException if existingCategory does not exist
            if (existingCategory == null) 
            {
                throw new ArgumentException($"The categoryId {categoryId} does not exists.");
            }
            // Verify that there are no Product associated with this category
            int categoryProductCount = existingCategory.Products.Count();
            if (categoryProductCount > 0)
            {
                throw new ArgumentException($"This cateogries has {categoryProductCount} products and cannot be deleted.");
            }
            // Remove the existingCategory from the database
            _westWindContext.Categories.Remove(existingCategory);
            int rowsDeleted = _westWindContext.SaveChanges();
            return rowsDeleted;
        }

        public void UndoChanges()
        {
            _westWindContext.ChangeTracker.Clear();
        }
    }
}
