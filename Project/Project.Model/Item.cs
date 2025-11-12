using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Item
    {

        public required int Id { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; }

        public required Category Category { get; set; }



        public Item(int id, string name, string description, Category category)
        {
            if (id < 0) throw new ArgumentException("Id nie może być wartością ujemną", nameof(id));

            Name = name;
            Description = description;
            Category = category;
        }

























    }
}
