using MRP.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.Entities
{
    public class MoveProducts
    {
        public string Id { get; set; }
        public Product product { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime MovimentDate { get; set; }
        public double Amount { get; set; }
        public EMoviment EMoviment { get; set; }
        public EPartner Partner { get; set; }

        public MoveProducts(Product product, string description, int quantity,
            DateTime movimentDate, double amount, EMoviment eMoviment, EPartner partner)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            this.product = product;
            Description = description;
            Quantity = quantity;
            MovimentDate = movimentDate;
            Amount = amount;
            EMoviment = eMoviment;
            Partner = partner;
        }

        public MoveProducts()
        {
        }
    }
}
