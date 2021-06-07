using MRP.Models.Enums;
using MRP.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public CNPJ CNPJ { get; set; }
        public Address Address { get; set; }
        public Email Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public EPartner PartnerType { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Customer(string id, CNPJ cNPJ, Address address, Email email, string phone, string description)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            CNPJ = cNPJ;
            Address = address;
            Email = email;
            Phone = phone;
            Description = description;
            PartnerType = EPartner.Customer;
        }

        public Customer()
        {
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
