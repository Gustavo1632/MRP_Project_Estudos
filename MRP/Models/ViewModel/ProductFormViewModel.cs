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
        public string ProductId { get; set; }
        public string Description { get; set; }
        public EProductType ProductType { get; set; }
        public int AvailableQuantity { get; set; }
        public double Price { get; set; }
        public string SupplierId { get; set; }
        public int Quantity { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; }


    }
}
