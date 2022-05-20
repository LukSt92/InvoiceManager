using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManager.Models.Domains
{
    public class Product
    {
        public Product()
        {
            InvoicePositions = new Collection<InvoicePosition>();
                        
        }

        public int Id { get; set; }

        [Display(Name = "Nazwa produktu")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Cena Netto")]
        public decimal NetPrice { get; set; }

        [Display(Name = "Stawka VAT")]
        public decimal VatRate { get; set; }

        [Display(Name = "Kwota VAT")]
        public decimal VatAmount { get; set; }

        [Display(Name = "Cena Brutto")]
        public decimal GrossPrice { get; set; }


        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }





        public ApplicationUser User { get; set; }
        public ICollection<InvoicePosition> InvoicePositions { get; set; }
    }
}