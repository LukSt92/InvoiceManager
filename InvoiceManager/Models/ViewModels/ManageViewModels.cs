using System.Collections.Generic;

namespace InvoiceManager.Models
{

    public class ManageViewModels
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}