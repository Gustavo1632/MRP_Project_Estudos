using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MRP.Data;
using MRP.Models.Entities;
using MRP.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Controllers
{
    public class MovimentRecordsController : Controller
    {
        private readonly MovimentRecordService _movimentRecordService;
        private readonly ProductService _productService;
        private readonly MRPContext _context;

        public MovimentRecordsController(MovimentRecordService movimentRecorService, MRPContext context, ProductService productService)
        {
            _movimentRecordService = movimentRecorService;
            _context = context;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _movimentRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
        public async Task<IActionResult> ByProductSearch(string description, DateTime? minDate, DateTime? maxDate)
        {

            var product = await _context.Product.FirstOrDefaultAsync(m => m.Description==description);
           

            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            if (description==null)
            {
                return BadRequest(new { message = "description não pode ser nula" });
            }


            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            ViewData["description"] = description.ToString();

            var result = await _movimentRecordService.FindByProduct(description, minDate, maxDate);
            return View(result);
        }

    }
}
