using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRP.Models.Enums;

namespace MRP.Models.ViewModel
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; }

       
    }
}
