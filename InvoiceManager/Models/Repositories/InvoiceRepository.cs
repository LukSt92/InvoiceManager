﻿using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class InvoiceRepository
    {
        public List<Invoice> GetInvoices(string userId)
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoice(int id, object userid)
        {
            throw new NotImplementedException();
        }

        public List<MethodofPayment> GetMethodsOfPayment()
        {
            throw new NotImplementedException();
        }

        public InvoicePosition GetInvoicePosition(int invoicePositionId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Add(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public void Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public void AddPosition(InvoicePosition invoicePosition, string userId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePosition(InvoicePosition invoicePosition, string userId)
        {
            throw new NotImplementedException();
        }

        public decimal UpdateInvoiceValue(int invoiceId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void DeletePosition(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}