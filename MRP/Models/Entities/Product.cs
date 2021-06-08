using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MRP.Models.Enums;

namespace MRP.Models.Entities
{
    public class Product
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3)]
        public string Description { get; set; }
        public EProductType ProductType { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();
        [Display(Name = "Available Quantity")]
        [Required(ErrorMessage = "{0} required")]
        public int AvailableQuantity { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierId { get; set; }
        public int Quantity { get; set; }
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public ICollection<MovimentRecord> MovimentsRecords { get; set; } = new List<MovimentRecord>();


        public Product(string id,string description, EProductType productType, int availableQuantity, double price, string supplierId)
        {
            Id = id;
            Description = description;
            ProductType = productType;
            AvailableQuantity = availableQuantity;
            Price = price;
            SupplierId = supplierId;

        }

        public Product()
        {
        }

        public override string ToString()
        {
            return Description;
        }




    }
}
