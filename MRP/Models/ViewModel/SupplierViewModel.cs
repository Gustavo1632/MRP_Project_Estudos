using MRP.Models.Entities;
using MRP.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.ViewModel
{
    public class SupplierViewModel
    {
        public string SupplierId { get; set; }
        public string Description { get; set; }
        public string CNPJNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public EStatus Status { get; set; }

    }
}
