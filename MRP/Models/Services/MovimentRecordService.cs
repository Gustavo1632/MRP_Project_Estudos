using Microsoft.EntityFrameworkCore;
using MRP.Data;
using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Models.Services
{
    public class MovimentRecordService
    {
        private readonly MRPContext _context;

        public MovimentRecordService(MRPContext context)
        {
            _context = context;
        }

        public async Task<List<MovimentRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.MovimentsRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.MovimentDate >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.MovimentDate <= maxDate.Value);
            }
            return await result.Include(x => x.Product).OrderByDescending(x => x.MovimentDate).
                ToListAsync();
        }

        public async Task<List<MovimentRecord>> FindByProduct(string description, DateTime? minDate, DateTime? maxDate)
        {

            var result = from obj in _context.MovimentsRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.MovimentDate >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.MovimentDate <= maxDate.Value);
            }
            if (description!=null)
            {
                result = result.Where(x => x.Product.Description.Contains(description));
            }

            return await result.Include(x => x.Product).OrderByDescending(x => x.MovimentDate).
                OrderByDescending(x => x.Product.Description).ToListAsync();
        }

    }
}
