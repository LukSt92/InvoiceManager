using InvoiceManager.Models;
using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using InvoiceManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private ProductRepository _productRepository = new ProductRepository();
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var invoices = _invoiceRepository.GetInvoices(userId);

            return View(invoices);
        }

        public ActionResult Invoice(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var invoice = id == 0 ?
                GetNewInvoice(userId) :
                _invoiceRepository.GetInvoice(id, userId);

            var vm = PrepareInvoiceVm(invoice, userId);

            return View(vm);
        }

        private EditInvoiceViewModel PrepareInvoiceVm(
            Invoice invoice, string userId)
        {
            return new EditInvoiceViewModel
            {
                Invoice = invoice,
                Heading = invoice.Id == 0 ? "Dodawanie nowej faktury" : "Faktura",
                Clients = _clientRepository.GetClients(userId),
                MethodofPayment = _invoiceRepository.GetMethodsOfPayment(),
            };
        }

        private Invoice GetNewInvoice(string userId)
        {
            return new Invoice
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                PaymentDate = DateTime.Now.AddDays(7)
            };
        }

        public ActionResult InvoicePosition(
            int invoiceId, int InvoicePositionId = 0)
        {
            var userId = User.Identity.GetUserId();

            var invoicePosition = InvoicePositionId == 0 ?
                GetNewPosition(invoiceId, InvoicePositionId) :
                _invoiceRepository.GetInvoicePosition(InvoicePositionId, userId);

            var vm = PrepareInvoicePositionVm(invoicePosition);

            return View(vm);
        }

        private EditInvoicePositionViewModel PrepareInvoicePositionVm(InvoicePosition invoicePosition)
        {
            var userId = User.Identity.GetUserId();

            return new EditInvoicePositionViewModel
            {
                InvoicePosition = invoicePosition,
                Heading = invoicePosition.Id == 0 ? "Dodawanie nowej pozycji" : "Pozycja",
                Products = _productRepository.GetProducts(userId),
                UnitofMeasures = _invoiceRepository.GetUnitsofMeasures()

            };
        }

        private InvoicePosition GetNewPosition(
            int invoiceId, int invoicePositionId)
        {
            return new InvoicePosition
            {
                InvoiceId = invoiceId,
                Id = invoicePositionId, 
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invoice(Invoice invoice)
        {
            var userId = User.Identity.GetUserId();
            invoice.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoiceVm(invoice, userId);
                return View("Invoice", vm);
            }

            if (invoice.Id == 0)
                _invoiceRepository.Add(invoice);
            else
                _invoiceRepository.Update(invoice);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoicePosition(InvoicePosition invoicePosition)
        {

            var userId = User.Identity.GetUserId();

            var product = _productRepository
                .GetProduct(invoicePosition.ProductId, userId);

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoicePositionVm(invoicePosition);
                return View("Invoice", vm);
            }

            

            invoicePosition.Value = invoicePosition.Quantity * product.GrossPrice;

            invoicePosition.NetValue = invoicePosition.Quantity * product.NetPrice;

            invoicePosition.VatValue = invoicePosition.Quantity * product.VatAmount;

            if (invoicePosition.Id == 0)
                _invoiceRepository.AddPosition(invoicePosition, userId);
            else
                _invoiceRepository.UpdatePosition(invoicePosition, userId);

            _invoiceRepository.UpdateInvoiceNetValue(invoicePosition.InvoiceId, userId);
            _invoiceRepository.UpdateInvoiceGrossValue(invoicePosition.InvoiceId, userId);


            return RedirectToAction("Invoice",
                new { id = invoicePosition.InvoiceId });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                //logowanie
                return Json(new { Success = false, Message = exception.Message }); ;
            }


            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult DeletePosition(int id, int invoiceId)
        {
            var invoiceNetValue = 0m;
            var invoiceGrossValue = 0m;


            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.DeletePosition(id, userId);
                 invoiceNetValue = _invoiceRepository.UpdateInvoiceNetValue(invoiceId, userId);
                invoiceGrossValue = _invoiceRepository.UpdateInvoiceGrossValue(invoiceId, userId);
            }
            catch (Exception exception)
            {
                //logowanie
                return Json(new { Success = false, Message = exception.Message }); ;
            }


            return Json(new { Success = true, InvoiceNetValue = invoiceNetValue, InvoiceGrossValue = invoiceGrossValue });
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Product()
        {
            var userId = User.Identity.GetUserId();

            var products = _productRepository.GetProducts(userId);

            return View(products);
        }

        public ActionResult AddEditProduct(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var product = id == 0 ?
                GetNewProduct(userId) :
                _productRepository.GetProduct(id, userId);

            var pvm = PrepareProductVm(product, userId);

            return View(pvm);
        }

        private EditProductViewModel PrepareProductVm(
            Product product, string userId)
        {
            return new EditProductViewModel
            {
                Product = product,
                Heading = product.Id == 0 ? "Dodawanie nowego produktu" : "Produkt",
            };
        }

        private Product GetNewProduct(string userId)
        {
            return new Product
            {
                UserId = userId,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditProduct(Product product)
        {
            var userId = User.Identity.GetUserId();
            product.UserId = userId;

            if (!ModelState.IsValid)
            {
                var pvm = PrepareProductVm(product, userId);

                return View("AddEditProduct", pvm);
            }

            if (product.Id == 0)
                _productRepository.Add(product);
            else
                _productRepository.Update(product);

            return RedirectToAction("Product");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _productRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                //logowanie
                return Json(new { Success = false, Message = exception.Message }); ;
            }


            return Json(new { Success = true });
        }

        public ActionResult Client()
        {
            var userId = User.Identity.GetUserId();

            var clients = _clientRepository.GetClients(userId);

            return View(clients);
        }

        public ActionResult AddEditClient (int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var client = id == 0 ?
                GetNewClient(userId) :
                _clientRepository.GetClient(id, userId);

            var cvm = PrepareClientVm(client, userId);

            return View(cvm);
        }

        private EditClientViewModel PrepareClientVm( Client client, string userId)
        {
            return new EditClientViewModel
            {
                Client = client,
                Heading = client.Id == 0 ? "Dodawanie nowego klienta" : "Klient",
            };
        }

        private Client GetNewClient(string userId)
        {
            return new Client
            {
                UserId = userId
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditClient(Client client)
        {
            var userId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                var cvm = PrepareClientVm(client, userId);

                return View("AddEditClient", cvm);
            }

            if (client.Id == 0)
                _clientRepository.Add(client);
            else
                _clientRepository.Update(client);

            return RedirectToAction("Client");
        }

        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _clientRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                //logowanie
                return Json(new { Success = false, Message = exception.Message }); ;
            }

            return Json(new { Success = true });
        }
    }
}