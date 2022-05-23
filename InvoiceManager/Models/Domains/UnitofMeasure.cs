using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Domains
{
    public class UnitofMeasure
    {
        public UnitofMeasure()
        {
            InvoicePositions = new Collection<InvoicePosition>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Jednostka miary")]
        public string Name { get; set; }


        public ICollection<InvoicePosition> InvoicePositions { get; set; }
    }
}