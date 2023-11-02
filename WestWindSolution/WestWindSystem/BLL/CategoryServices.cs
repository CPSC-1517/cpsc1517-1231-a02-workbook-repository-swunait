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
        private WestWindContext _westWindContext;

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
    }
}
