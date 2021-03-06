using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Domains
{
    public class Invoice
    {
        public Invoice()
        {
            InvoicePositions = new Collection<InvoicePosition>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Tytuł jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Wartość netto")]
        public decimal NetValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Wartość brutto")]
        public decimal GrossValue { get; set; }

        [Required(ErrorMessage = "Pole Sposób płatności jest wymagane")]
        [Display(Name = "Sposób płatności")]
        public int MethodOfPaymentId { get; set; }

        [Required(ErrorMessage = "Pole Data płatności jest wymagane")]
        [Display(Name = "Data płatności")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Uwagi")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "Pole Klient jest wymagane")]
        [Display(Name = "Klient")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        
        public MethodofPayment MethodofPayment { get; set; }
        public Client Client { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<InvoicePosition> InvoicePositions { get; set; }
    }
}