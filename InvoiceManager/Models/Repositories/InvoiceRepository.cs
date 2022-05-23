using InvoiceManager.Models.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace InvoiceManager.Models.Repositories
{
    public class InvoiceRepository
    {
        public List<Invoice> GetInvoices(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Invoices
                    .Include(x => x.Client)
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }

        public Invoice GetInvoice(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Invoices
                    .Include(x => x.InvoicePositions)
                    .Include(x => x.InvoicePositions
                    .Select(y => y.Product))
                    .Include(x => x.InvoicePositions
                    .Select(y => y.UnitofMeasure))
                    .Include(x => x.MethodofPayment)
                    .Include(x => x.User)
                    .Include(x => x.User.Address)
                    .Include(x => x.Client)
                    .Include(x => x.Client.Address)
                    .Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public List<MethodofPayment> GetMethodsOfPayment()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.MethodofPayments.ToList();
            }
        }

        public InvoicePosition GetInvoicePosition(int invoicePositionId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.InvoicePositions
                    .Include(x => x.Invoice)
                    .Single(x => x.Id == invoicePositionId && x.Invoice.UserId == userId);
            }
        }

        public void Add(Invoice invoice)
        {
            using (var context = new ApplicationDbContext())
            {
                invoice.CreatedDate = DateTime.Now;
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
        }

        public void Update(Invoice invoice)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoiceToUpdate = context.Invoices.Single(x => x.Id == invoice.Id && x.UserId == invoice.UserId);

                invoiceToUpdate.ClientId = invoice.ClientId;
                invoiceToUpdate.Comments = invoice.Comments;
                invoiceToUpdate.MethodOfPaymentId = invoice.MethodOfPaymentId;
                invoiceToUpdate.PaymentDate = invoice.PaymentDate;
                invoiceToUpdate.Title = invoice.Title;

                context.SaveChanges();
            }
        }

        public void AddPosition(InvoicePosition invoicePosition, string userId)
        {

            using (var context = new ApplicationDbContext())
            {
                var invoice = context.Invoices
                    .Single(x =>
                    x.Id == invoicePosition.InvoiceId &&
                    x.UserId == userId);


                context.InvoicePositions.Add(invoicePosition);
                context.SaveChanges();
            }
        }

        public List<UnitofMeasure> GetUnitsofMeasures()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.UnitofMeasures.ToList();
            }
        }

        public void UpdatePosition(InvoicePosition invoicePosition, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var positionToUpdate = context.InvoicePositions
                    .Include(x => x.Product)
                    .Include(x => x.Invoice)
                    .Single(x =>
                        x.Id == invoicePosition.Id && x.Invoice.UserId == userId);

                positionToUpdate.Lp = invoicePosition.Lp;
                positionToUpdate.ProductId = invoicePosition.ProductId;
                positionToUpdate.Quantity = invoicePosition.Quantity;
                positionToUpdate.UnitofMeasureId = invoicePosition.UnitofMeasureId;
                positionToUpdate.Value = positionToUpdate.Product.GrossPrice * invoicePosition.Value;
                positionToUpdate.NetValue = invoicePosition.NetValue;
                positionToUpdate.VatValue = invoicePosition.VatValue;

                context.SaveChanges();

            }
        }

        public decimal UpdateInvoiceNetValue(int invoiceId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoice = context.Invoices
                    .Include(x => x.InvoicePositions)
                    .Single(x => x.Id == invoiceId && x.UserId == userId);

                invoice.NetValue = invoice.InvoicePositions.Sum(x => x.NetValue);

                context.SaveChanges();
                return invoice.NetValue;
            }
        }

        public decimal UpdateInvoiceGrossValue(int invoiceId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoice = context.Invoices
                    .Include(x => x.InvoicePositions)
                    .Single(x => x.Id == invoiceId && x.UserId == userId);

                invoice.GrossValue = invoice.InvoicePositions.Sum(x => x.NetValue) + invoice.InvoicePositions.Sum(x => x.VatValue);

                context.SaveChanges();
                return invoice.GrossValue;
            }
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoiceToDelete = context.Invoices
                    .Single(x => x.Id == id && x.UserId == userId);

                context.Invoices.Remove(invoiceToDelete);

                context.SaveChanges();
            }
        }

        public void DeletePosition(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var positionToDelete = context.InvoicePositions
                    .Include(x => x.Invoice)
                    .Single(x => x.Id == id && x.Invoice.UserId == userId);

                context.InvoicePositions.Remove(positionToDelete);

                context.SaveChanges();
            }
        }
    }
}