using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.ValueObjects
{
   
    public class Address 
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string SupplierId { get; set; }

        public Address(string street, string number, string district, string city,
            string state, string country, string zipCode, string supplierId)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            SupplierId = supplierId;
        }

        public Address()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
        }

        

        public override string ToString()
        {
            return $"{Street}, {Number}, - {City}/{State}";
        }
    }
}
