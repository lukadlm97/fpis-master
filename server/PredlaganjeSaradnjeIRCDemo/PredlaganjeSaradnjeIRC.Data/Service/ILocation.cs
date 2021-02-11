using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface ILocation
    {
        Location GetByCompanyId(int companyId);
        bool Add(int companyId, Location newLocation);

        Location GetInserted();
    }
}
