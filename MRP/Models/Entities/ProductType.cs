using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.Entities
{
    public class ProductType
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public ProductType(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public ProductType()
        {
        }

        public override string ToString()
        {
            return Description;
        }

    }
}
