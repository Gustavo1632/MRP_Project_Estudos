using Microsoft.EntityFrameworkCore;
using MRP.Data;
using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRP.Models.Enums;


namespace MRP.Models.Services
{
    public class ProductService
    {
        private readonly MRPContext _context;

        public ProductService(MRPContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Product prod)
        {
            _context.Add(prod);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> FindAllAsync()
        {
            return await _context.Product.OrderBy(x => x.Description).ToListAsync();
        }

        public async Task<Product> FindByIdAsync(string id)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(string  id)
        {
            var obj = await _context.Product.FindAsync(id);
            _context.Product.Remove(obj);
            await _context.SaveChangesAsync();
        }


        public void ReceiveParts(Product product, int quantity)
        {
            product.AvailableQuantity += quantity;
            product.Quantity = quantity;
            double amount = (double)quantity * product.Price;
            product.MovimentsRecords.Add(new MovimentRecord(product.Description, product, 
                quantity, DateTime.Now, amount, EMoviment.Receiving));
        }


        public void ShipParts(Product product, int quantity)
        {
            product.AvailableQuantity -= quantity;
            product.Quantity = quantity;
            double amount = (double)quantity * product.Price;
            product.MovimentsRecords.Add(new MovimentRecord(product.Description, product, 
                product.Quantity, DateTime.Now, amount, EMoviment.Receiving));
        }
    }
}
