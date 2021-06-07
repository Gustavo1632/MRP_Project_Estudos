using Microsoft.EntityFrameworkCore;
using MRP.Data;
using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.Services
{
    public class ProductTypeService
    {
        private readonly MRPContext _context;

        public ProductTypeService(MRPContext context)
        {
            _context = context;
        }

        public async Task<List<ProductType>> FindAllAsync()
        {
            return await _context.ProductType.OrderBy(x => x.Description).ToListAsync();
        }

        public async Task InsertAsync(ProductType prod)
        {
            _context.Add(prod);
            await _context.SaveChangesAsync();
        }
    }
}
