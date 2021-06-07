using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.ViewModel
{
    public class IncreaseStockFormViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
