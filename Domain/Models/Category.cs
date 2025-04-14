using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        //public ICollection<CarCategory> CarCategories { get; private set; }

        public Category()
        {
            
        }

        //public Category(string name, string? description=null)
        //{
        //    Id = Guid.NewGuid();
        //    Name = name;
        //    Description = description;
        //    CarCategories = new List<CarCategory>();
        //}
    }
}
