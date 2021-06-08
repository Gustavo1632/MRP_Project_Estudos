using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MRP.Data;
using MRP.Models.Entities;
using MRP.Models.Services;
using MRP.Models.ValueObjects;
using MRP.Models.ViewModel;

namespace MRP.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly MRPContext _context;
        private readonly SupplierService _supplierService;

        public SuppliersController(MRPContext context, SupplierService supplierService)
        {
            _context = context;
            _supplierService = supplierService;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            var supl = new List<SupplierViewModel>();

            var suppliers = await _supplierService.FindAllAsync();

            foreach (Supplier x in suppliers)
            {
                supl.Add(new SupplierViewModel
                {
                    SupplierId = x.Id,
                    Description = x.Description,
                    CNPJNumber = x.CNPJ.Number,
                    EmailAddress = x.Email.Address,
                    Status = x.Status
                });
            };
            return View(supl);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var supplier = await _context.Supplier.Include(x => x.Email).Include(x => x.CNPJ).
               Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);

            var viewmodel = new SupplierViewModel
            {
                SupplierId=supplier.Id,
                Description = supplier.Description,
                CNPJNumber = supplier.CNPJ.Number,
                EmailAddress = supplier.Email.Address,
                Status = supplier.Status
            };

            if (supplier == null)
                return NotFound();

            return View(viewmodel);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierVM)
        {
            supplierVM.SupplierId = _supplierService.GetId();

            var erros = _supplierService.ValidateSuplier(supplierVM);

            if (erros.Any())
            {
                return new ObjectResult(new { message = erros, HasError = true }); ;
            }

            var cnpj = new CNPJ(supplierVM.CNPJNumber, supplierVM.SupplierId);
            var email = new Email(supplierVM.EmailAddress, supplierVM.SupplierId);

            var address = new Address(supplierVM.Street, supplierVM.Number,
            supplierVM.District, supplierVM.City, supplierVM.State, supplierVM.Country,
            supplierVM.ZipCode, supplierVM.SupplierId);


            var supplier = new Supplier(supplierVM.SupplierId, cnpj, address, email,
                supplierVM.Phone, supplierVM.Description);

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            await _supplierService.InsertAsync(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            //var supplier = await _context.Supplier.FindAsync(id);
            var sup =  _supplierService.FindById(id);

            if (sup == null)
                return NotFound();

            var viewModel = new SupplierViewModel
            {
                SupplierId = sup.Id,
                Description = sup.Description,
                CNPJNumber = sup.CNPJ.Number,
                EmailAddress = sup.Email.Address,
                Street = sup.Address.Street,
                Number = sup.Address.Number,
                District = sup.Address.District,
                City = sup.Address.City,
                State = sup.Address.State,
                Country = sup.Address.Country,
                ZipCode = sup.Address.ZipCode,
                Phone = sup.Phone,
                Status = sup.Status
            };

            return View(viewModel);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SupplierViewModel supplierVM)
        {
            if (id != supplierVM.SupplierId)
                return NotFound();

            var supplier =  _supplierService.FindById(supplierVM.SupplierId);
                       
            var errorList = _supplierService.EditEmail(supplierVM, supplier);

            errorList.AddRange(_supplierService.EditCNPJ(supplierVM, supplier));

            _supplierService.EditAddress(supplierVM, supplier);

            if (errorList.Any())
            {
                return new ObjectResult(new { message = errorList, HasError = true });
            }

            _supplierService.EditDescriptionAndPhone(supplierVM, supplier);

            _supplierService.StatusUpdate(supplierVM, supplier);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
                        return NotFound();

                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplierVM);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(string id = null)
        {
            if (id == null)
                return NotFound();

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(string id)
        {
            return _context.Supplier.Any(e => e.Id == id);
        }
               
    }
}
