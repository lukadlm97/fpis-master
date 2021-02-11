using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface IContact
    {
        Contact GetById(int companyId,int contactId);
        IEnumerable<Contact> GetAll(int companyId);
        IEnumerable<Contact> GetAll();

        bool Update(int companyId, int contactId, Contact updatedContact);
        bool Delete(int id, int contactId);

        Contact GetInserted();
    }
}
