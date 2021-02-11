using Microsoft.EntityFrameworkCore;
using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Services
{
    public class ContactService : IContact
    {
        private readonly ApplicationContext _context;
        public ContactService(ApplicationContext context)
        {
            _context = context;
        }
        
        // get all contacts for company (fixed)
        public IEnumerable<Contact> GetAll(int companyId)
        {
            var company = _context.Companies
                .Include(company => company.Contacts)
                .FirstOrDefault(company => company.Id == companyId);

            if(company == null || company.Contacts == null)
            {
                return null;
            }
            _context.Entry(company).State = EntityState.Detached;

            return company.Contacts;
        }
      
        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts;
        }

        // TODO: test update and delete for contacts
        public bool Update(int companyId, int contactId, Contact updatedContact)
        {
            var company = _context.Companies
                                .Include(company => company.Contacts)
                                .FirstOrDefault(company => company.Id == companyId);

            if(company == null)
            {
                return false;
            }

            Contact forUpdate = FindContactById(company, contactId);

            if(forUpdate == null)
            {
                return false;
            }

            UpdateContact(ref forUpdate, updatedContact);

            try
            {
                _context.Update(forUpdate);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool Delete(int idCompany, int contactId)
        {
            var contact = GetById(idCompany,contactId);

            if(contact == null)
            {
                return false;
            }

            try
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        
        // Helper functions
        private void UpdateContact(ref Contact forUpdate, Contact updatedContact)
        {
            forUpdate.Content = updatedContact.Content;
            forUpdate.ContactType = updatedContact.ContactType;
        }
        public Contact FindContactById(Company company,int id)
        {
            return company.Contacts
                .FirstOrDefault(contact => contact.Id == id); 
        }
        public Contact GetById(int companyId, int contactId)
        {
            var contacts = GetAll(companyId);

            if(contacts == null)
            {
                return null;
            }

            return contacts.FirstOrDefault(c => c.Id == contactId);
        }

        public Contact GetInserted()
        {
            return GetAll().LastOrDefault();
        }
    }
}
