using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MRP.Models.ValueObjects
{
    public class CNPJ :Notifiable<Notification>
    {
        public string Id { get; set; }
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ size should be14 caracteres")]
        public string Number { get; set; }
        public string SupplierId { get; set; }

        public CNPJ(string number, string supplierId)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            Number = number;
            SupplierId = supplierId;
                      
        }

        public CNPJ()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

        }

        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        public override string ToString()
        {
            return Number;
        }
    }
}
