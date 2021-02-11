using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface IRequestForCooperation
    {
        // main info about Propspal
        RequestForCooperation GetById(int id);
        IEnumerable<RequestForCooperation> GetAll();

        // CRUD operation 
        bool Add(RequestForCooperation newProposalForCooperation);
        bool Delete(int id);
        bool Update(int id,RequestForCooperation proposalForCooperation);

        // bonus operation
        bool UpgradeDescription(int id, string description);

        RequestForCooperation GetLastInsterted();

    }
}
