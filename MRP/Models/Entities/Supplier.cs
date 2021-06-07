using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MRP.Models.ValueObjects;
using MRP.Models.Enums;
using Flunt.Notifications;
using MRP.Models.ValueObjects.Contracts;

namespace MRP.Models.Entities
{
    public class Supplier : Notifiable<Notification>
    {
        public string Id { get; set; }
        public CNPJ CNPJ { get; set; }
        public Address Address { get; set; }
        public Email Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public EPartner PartnerType { get; set; }
        public EStatus Status { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Supplier(string id, CNPJ cNPJ, Address address,
            Email email, string phone, string description)
        {            
            Id = id;
            CNPJ = cNPJ;
            Address = address;
            Email = email;
            Phone = phone;
            Description = description;
            PartnerType = EPartner.Supplier;
            Status = EStatus.Active;

            AddNotifications(new CreateEmailContract(email));

        }

        public Supplier()
        {
        }



        public void AddProduct(Product product)
        {
            Products.Add(product);
        }



        // Adicionar CNPJ do fornecedor e fazer a validação do CNPJ
        // fazer a pesquisa de produtos lincados ao fornecedor
        // pesquisar movimentos por fornecedor

    }
}
