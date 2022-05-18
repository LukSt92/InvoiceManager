using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class ClientRepository
    {
        public List<Client> GetClients(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients
                    .Include(x => x.Address)
                    .Where(x => x.UserId == userId).ToList();
            }
        }
        public Client GetClient(int clientId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients.Single(x => x.Id == clientId && x.UserId == userId);
            }
        }

        public void Add(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }

        public void Update(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToUpdate = context.Clients.Single(x => x.Id == client.Id && x.UserId == client.UserId);

                clientToUpdate.Name = client.Name;
                clientToUpdate.Address.PostalCode = client.Address.PostalCode;
                clientToUpdate.Address.City = client.Address.City;
                clientToUpdate.Address.Street = client.Address.Street;
                clientToUpdate.Address.Number = client.Address.Number;
                clientToUpdate.Email = client.Email;

                context.SaveChanges();
            }
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToDelete = context.Clients
                    .Single(x => x.Id == id && x.UserId == userId);

                context.Clients.Remove(clientToDelete);

                context.SaveChanges();
            }
        }
    }
}