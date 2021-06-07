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
        public int ProductTypeId { get; set; }
        public string ProductTypeDescription { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();
        [Display(Name = "Available Quantity")]
        [Required(ErrorMessage = "{0} required")]
        public int AvailableQuantity { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public ICollection<MovimentRecord> MovimentsRecords { get; set; } = new List<MovimentRecord>();


        public Product(string description, int productTypeId, int availableQuantity, double price, int supplierId, string productDescription)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            Description = description;
            ProductTypeId = productTypeId;
            AvailableQuantity = availableQuantity;
            Price = price;
            SupplierId = supplierId;
            ProductTypeDescription = productDescription;
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
