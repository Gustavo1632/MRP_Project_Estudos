using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MRP.Models.Enums;

namespace MRP.Models.Entities
{
    public class MovimentRecord
    {
        public string Id { get; set; }
        public Product Product { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string Description { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MovimentDate { get; set; }
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        [Display(Name = "Moviment")]
        public EMoviment EMoviment { get; set; }
        public Supplier Supplier { get; set; }
        


        public MovimentRecord(string description, Product product, int quantity, DateTime movimentDate, double amount, EMoviment eMoviment)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            Description = description;
            Product = product;
            Quantity = quantity;
            MovimentDate = movimentDate;
            Amount = amount;
            EMoviment = eMoviment;
            
        }

        public MovimentRecord()
        {
        }
    }
}
