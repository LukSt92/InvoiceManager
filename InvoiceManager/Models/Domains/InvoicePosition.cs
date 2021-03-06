using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models.Domains
{
    public class InvoicePosition
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal NetValue { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal VatValue { get; set; }

        [Required(ErrorMessage = "Pole Produkt jest wymagane")]
        [Display(Name = "Produkt")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Pole Ilość jest wymagane")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Pole Jednostka miary jest wymagane")]
        [Display(Name = "Jednostka miary")]
        public int UnitofMeasureId { get; set; }

        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public UnitofMeasure UnitofMeasure { get; set; }
    }
}