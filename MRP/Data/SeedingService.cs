using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Data
{
    public class SeedingService
    {
        private MRPContext _context;

        public SeedingService(MRPContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.ProductType.Any())
            {
                return; // DB has been seeded
            }

            ProductType p1 = new (1, "ManufacturedComponent");
            ProductType p2 = new (2, "Imported");
            ProductType p3 = new (3, "NationalPurchased ");
            ProductType p4 = new (4, "Benefited");
            ProductType p5 = new (5, "FinishGoods");
            
            _context.ProductType.AddRange(p1, p2, p3, p4,p5);
            _context.SaveChanges();
        }
    }
}
    
         