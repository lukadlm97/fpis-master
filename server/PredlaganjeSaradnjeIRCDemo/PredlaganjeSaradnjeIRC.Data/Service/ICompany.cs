using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface ICompany
    {
        // main info about Company
        Company GetById(int id);
        IEnumerable<Company> GetAll();
        Company GetLastInserted();

        // CRUD operation
        bool Add(Company newCompany);
        bool Update(int id,Company company);
        bool Delete(int id);
        bool AddNewContact(int id, Contact newContact);

        // more info about Company
        Location GetLocation(int id);
        object GetInserted();
    }
}
