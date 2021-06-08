using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRP.Data;
using MRP.Models.Entities;
using MRP.Models.Enums;
using MRP.Models.Services;
using MRP.Models.ViewModel;

namespace MRP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MRPContext _context;
        private readonly ProductService _productService;
        private readonly SupplierService _supplierService;

        public ProductsController(MRPContext context, ProductService productService, SupplierService supplierService)
        {
            _context = context;
            _productService = productService;
            _supplierService = supplierService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var prod = new List<ProductFormViewModel>();

            var products = await _productService.FindAllAsync();

            foreach (Product x in products)
            {
                prod.Add(new ProductFormViewModel
                {
                    ProductId = x.Id,
                    Description = x.Description,
                    ProductType = x.ProductType,
                    AvailableQuantity = x.AvailableQuantity,
                    Price = x.Price
                });
            }
            return View(prod);
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            var viewModel = new ProductFormViewModel
            {
                ProductId = product.Id,
                Description = product.Description,
                ProductType = product.ProductType,
                AvailableQuantity = product.AvailableQuantity,
                Price = product.Price

            };
            if (product == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }



        // GET: Products/Create
        public async Task<IActionResult> CreateAsync()
        {
            //var productTypes = await _productTypeService.FindAllAsync();
            var suppliers = await _supplierService.FindAllAsync();
            var viewModel = new ProductFormViewModel { Suppliers = suppliers, ProductTypes = _productService.ProductTypeList() };

            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel productVM)
        {
            productVM.ProductId = _supplierService.GetId();

            var productTypes = _productService.ProductTypeList();

            var product = new Product(productVM.ProductId, productVM.Description, productVM.ProductType,
                productVM.AvailableQuantity, productVM.Price, productVM.SupplierId);

            if (!ModelState.IsValid)
            {
                //var productTypes = await _productTypeService.FindAllAsync();
                var suppliers = await _supplierService.FindAllAsync();
                var viewModel = new ProductFormViewModel { Suppliers = suppliers, ProductTypes = productTypes };
                return View(viewModel);
            }
            //product.Id = new Guid();
            await _productService.InsertAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var productTypes = _productService.ProductTypeList();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            product.ProductTypes = productTypes;

            var viewModel = new ProductFormViewModel
            {
                ProductId = product.Id,
                Description = product.Description,
                ProductType = product.ProductType,
                AvailableQuantity = product.AvailableQuantity,
                Price = product.Price,
                SupplierId = product.SupplierId,
                Quantity = product.Quantity,
                Suppliers = product.Suppliers,
                ProductTypes = product.ProductTypes
            };


            if (product == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProductFormViewModel productVM)
        {
            if (id != productVM.ProductId)
            {
                return NotFound();
            }

            var productTypes = _productService.ProductTypeList();
            //product.ProductType = productTypes.FirstOrDefault(x => x.Description == product.Description);

            var product = _productService.FindById(productVM.ProductId);
            product.Description = productVM.Description;
            product.ProductType = productVM.ProductType;
            product.Price = productVM.Price;

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProductExists(productVM.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public async Task<IActionResult> IncreaseStockAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IncreaseStock(string id, int quantity)
        {
            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);

            if (id != product.Id)
            {
                return NotFound();
            }
            if (quantity <= 0)
            {
                return BadRequest(new { message = "Quantidade deve ser maior que 0" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productService.ReceiveParts(product, quantity);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> FindMoviments()
        {
            return View(await _context.Product.ToListAsync());
        }

    }
}
