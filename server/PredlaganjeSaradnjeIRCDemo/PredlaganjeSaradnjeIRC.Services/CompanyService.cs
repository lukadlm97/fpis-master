using Microsoft.EntityFrameworkCore;
using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PredlaganjeSaradnjeIRC.Services
{
    public class CompanyService : ICompany
    {
        private readonly ApplicationContext _context;
        private readonly ICity cityService;

        public CompanyService(ApplicationContext context)
        {
            _context = context;
            cityService = new CityService(context);
        }

        // main info about companies (fixed)
        public IEnumerable<Company> GetAll()
        {
            return _context.Companies
                .Include(company => company.Contacts)
                .Include(company => company.Locations)
                    .ThenInclude(location => location.City);
        }
        public Company GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(company => company.Id == id);
        }
       
        // crud operation (insert,update,delete) (fixed)
        public bool Add(Company newCompany)
        {
            PrepareCityOfLocationForSaving(ref newCompany);

            try
            {
                _context.Add(newCompany);
                _context.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public bool Delete(int id)
        {
            Company company = GetById(id);

            if(company == null)
            {
                return false;
            }

            try
            {
                _context.Remove(company);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool Update(int id,Company updatedCompany)
        {
            var company = GetById(id);

            if(company == null)
            {
                return false;
            }

            UpdateCompany(ref company, updatedCompany);

            try
            {
                _context.Update(company);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        // add new contact (fixed)
        public bool AddNewContact(int id, Contact newContact)
        {
            var company = GetById(id);

            if (company == null)
            {
                return false;
            }

            List<Contact> contacts = company.Contacts.ToList();
            contacts.Add(newContact);
            company.Contacts = (IEnumerable<Contact>)contacts;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            { 
                return false;
            }
            return true;
        }


        //helper method
        private void PrepareCityOfLocationForSaving(ref Company newCompany)
        {
            if (newCompany.Locations.Count() == 0)
                return;

            Location locationOfNewCompany = newCompany.Locations
                                                        .FirstOrDefault();

            locationOfNewCompany.City = cityService
                                                .GetById(locationOfNewCompany.City.Id);
        }
        public Location GetLocation(int companyId)
        {
            return GetById(companyId)
                 .Locations
                    .LastOrDefault();
        }
        public bool SetNewAddress(int id, Location location)
        {
            var company = GetById(id);

            if(company == null)
            {
                return false;
            }

            List<Location> address = company.Locations.ToList(); 
            company.Locations = UpdateAddress(address, location);

            try
            {
                _context.Update(company);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public Company GetLastInserted()
        {
            return _context.Companies
                .LastOrDefault();
        }
        private IEnumerable<Location> UpdateAddress(List<Location> address, Location location)
        {
            address.Add(location);
            return (IEnumerable<Location>)address;
        }
        private void UpdateCompany(ref Company companyForUpdate, Company updatedCompany)
        {
            companyForUpdate.Name = updatedCompany.Name;
            companyForUpdate.Username = updatedCompany.Username;
            companyForUpdate.Password = updatedCompany.Password;
        }

        public object GetInserted()
        {
            return GetAll().LastOrDefault();
        }
    }
}
