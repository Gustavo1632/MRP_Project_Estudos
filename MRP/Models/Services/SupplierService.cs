using MRP.Data;
using MRP.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MRP.Models.ValueObjects;
using MRP.Models.ViewModel;
using MRP.Models.Enums;

namespace MRP.Models.Services
{
    public class SupplierService
    {
        private readonly MRPContext _context;


        public SupplierService(MRPContext context)
        {
            _context = context;
        }

        public string GetId()
        {
            var id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            return id;
        }

        public async Task<List<Supplier>> FindAllAsync()
        {
            return await _context.Supplier.Include(x => x.CNPJ).Include(x => x.Email).Include(x => x.Address).
                OrderBy(x => x.CNPJ).ToListAsync();
        }

        public  Supplier FindById(string id)
        {
            return _context.Supplier.Include(x => x.Email).Include(x => x.CNPJ).
                Include(x => x.Address).FirstOrDefault(x => x.Id == id);
        }

        public async Task InsertAsync(Supplier supplier)
        {
            _context.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public string AddressComplet(Address address)
        {
            var addressComplet = address.Street + address.Number + address.District + address.City + address.State +
                address.Country + address.ZipCode;

            return addressComplet;
        }

        public bool CNPJExists(string cnpjNumber)
        {
            var listCNPJ = _context.CNPJ.AsNoTracking().ToList();

            if (listCNPJ.Any(x => x.Number == cnpjNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EmailExists(string emailAdress)
        {
            var listEmail = _context.Email.AsNoTracking().ToList();


            if (listEmail.Any(x => x.Address == emailAdress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EmailIsEqual(SupplierViewModel supplierVM)
        {
            var supplier = FindById(supplierVM.SupplierId);

            if (supplier.Email.Address == supplierVM.EmailAddress)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CNPJIsEqual(SupplierViewModel supplierVM)
        {
            var supplier = FindById(supplierVM.SupplierId);

            if (supplier.CNPJ.Number == supplierVM.CNPJNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddressIsEqual(SupplierViewModel supplierVM)
        {
            var supplier = FindById(supplierVM.SupplierId);

            var address = AddressComplet(supplier.Address);
            var address2 = AddressComplet(new Address(supplierVM.Street, supplierVM.Number, supplierVM.District, supplierVM.City,
                supplierVM.State, supplierVM.Country, supplierVM.ZipCode, supplierVM.SupplierId));

            if (address == address2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DescriptionIsEqual(SupplierViewModel supplierVM)
        {
            var supplier = FindById(supplierVM.SupplierId);

            if (supplier.Description == supplierVM.Description)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PhoneIsEqual(SupplierViewModel supplierVM)
        {
            var supplier = FindById(supplierVM.SupplierId);

            if (supplier.Phone == supplierVM.Phone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> EditEmail(SupplierViewModel supplierVM, Supplier supplier)
        {
            var errorList = new List<string>();

            if (EmailIsEqual(supplierVM) == false)
            {
                if (EmailExists(supplierVM.EmailAddress) == true)
                    errorList.Add("Email Já Cadastrado");

                var email = new Email(supplierVM.EmailAddress, supplierVM.SupplierId);

                if (!email.IsValid)
                    errorList.Add("Email Inválido");

                if (!errorList.Any())
                {
                    _context.Email.Add(email);
                    supplier.Email = email;
                }

                return errorList;
            }
            return errorList;
        }

        public List<string> EditCNPJ(SupplierViewModel supplierVM, Supplier supplier)
        {
            var errorList = new List<string>();

            if (CNPJIsEqual(supplierVM) == false)
            {
                if (CNPJExists(supplierVM.CNPJNumber) == true)
                    errorList.Add("CNPJ Já Cadastrado");

                var cnpj = new CNPJ(supplierVM.CNPJNumber, supplierVM.SupplierId);

                if (CNPJ.IsCnpj(cnpj.Number) == false)
                    errorList.Add("CNPJ Inválido");

                if (!errorList.Any())
                {
                    _context.CNPJ.Add(cnpj);
                    supplier.CNPJ = cnpj;
                }
                return errorList;
            }
            return errorList;
        }

        public void EditAddress(SupplierViewModel supplierVM, Supplier supplier)
        {
            if (AddressIsEqual(supplierVM) == false)
            {
                var address = new Address(supplierVM.Street, supplierVM.Number, supplierVM.District, supplierVM.City,
                supplierVM.State, supplierVM.Country, supplierVM.ZipCode, supplierVM.SupplierId);
                _context.Address.Add(address);
                supplier.Address = address;
            }
        }

        public void EditDescriptionAndPhone(SupplierViewModel supplierVM, Supplier supplier)
        {
            if (DescriptionIsEqual(supplierVM) == false)
                supplier.Description = supplierVM.Description;
            if (PhoneIsEqual(supplierVM) == false)
                supplier.Phone = supplierVM.Phone;
        }

        public void StatusUpdate(SupplierViewModel supplierVM, Supplier supplier)
        {
            if (supplierVM.Status == EStatus.Inactive)
                supplier.Status = EStatus.Inactive;

            else
            {
                supplier.Status = EStatus.Active;
                supplierVM.Status = EStatus.Active;
            }
        }

        public List<string> ValidateSuplier(SupplierViewModel supplierVM)
        {
            var listaDeErros = new List<string>();

            if (CNPJExists(supplierVM.CNPJNumber) == true)
                listaDeErros.Add("CNPJ Já Cadastrado");

            if (CNPJ.IsCnpj(supplierVM.CNPJNumber) == false)
                listaDeErros.Add("CNPJ Inválido");

            if (EmailExists(supplierVM.EmailAddress) == true)
                listaDeErros.Add("Email Já Cadastrado");

            var email = new Email(supplierVM.EmailAddress, supplierVM.SupplierId);

            if (!email.IsValid)
                listaDeErros.Add("Email Inválido");


            return listaDeErros;
        }


    }
}
