using Flunt.Notifications;
using MRP.Models.ValueObjects.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MRP.Models.ValueObjects
{
    public class Email : Notifiable<Notification>
    {
        public string Id { get; set; }
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Address { get; set; }
        public string SupplierId { get; set; }

        public Email(string address, string supplierId)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            Address = address;
            SupplierId = supplierId;

            AddNotifications(new CreateEmailContract(this));

                     
        }

        public Email()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
