using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public ICollection<Car> Cars { get; private set; }

        public Category()
        {
            
        }

        public Category(string name, string? description=null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
        }
    }
}
