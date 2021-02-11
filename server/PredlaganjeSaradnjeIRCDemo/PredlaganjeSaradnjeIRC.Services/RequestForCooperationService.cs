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
    public class RequestForCooperationService:IRequestForCooperation
    {
        private readonly ApplicationContext _context;
        private readonly ICompany companyService;
        private readonly IEmployee employeeService;
        private readonly IContact contactService;

        public RequestForCooperationService(ApplicationContext context)
        {
            _context = context;
            companyService = new CompanyService(context);
            employeeService = new EmployeeService(context);
            contactService = new ContactService(context);
        }
        
        //main info about prospal
        public IEnumerable<RequestForCooperation> GetAll()
        {
            return _context.ProposalForCooperations
                .Include(proposal => proposal.Company)
                    .ThenInclude(company => company.Contacts)
                .Include(proposal => proposal.Company)
                    .ThenInclude(company => company.Locations)
                        .ThenInclude(location =>location.City)
                .Include(proposal => proposal.Employee);
        }
        public RequestForCooperation GetById(int id)
        {
            return GetAll().
                FirstOrDefault(proposal => proposal.Id == id);
        }

        //CRUD operation
        public bool Add(RequestForCooperation newProposalForCooperation)
        {
            if(newProposalForCooperation == null)
            {
                return false;
            }

            var company = companyService
                .GetById(newProposalForCooperation.Company.Id);


            var employee = employeeService
                .GetById(newProposalForCooperation.Employee.Id);

            if(company == null || employee == null)
            {
                return false;
            }

            SetProposalsObjects(ref newProposalForCooperation, company, employee);

            try
            {
                _context.Add(newProposalForCooperation);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool Delete(int id)
        {
            var prospalForCooperation = GetById(id);

            if(prospalForCooperation == null)
            {
                return false;
            }

            try
            {
                _context.Remove(prospalForCooperation);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool Update(int id,RequestForCooperation proposalForCooperation)
        {
            var proposal = GetById(id);

            if(proposal == null)
            {
                return false;
            }

            if (!CheckObjectsForUpdate(proposalForCooperation))
            {
                return false;
            }

            SetProposalsAttributesUpdate(ref proposal, proposalForCooperation);

            proposal.Company.Contacts = contactService.GetAll(proposal.Company.Id);
            

            try
            {
                //_context.Entry(proposal).State = EntityState.Detached;
                _context.Update(proposal);
                _context.SaveChanges();
            }catch(Exception e)
            {
                return false;
            }

            return true;
        }
        
        //bonus 
        public bool UpgradeDescription(int id, string description)
        {
            var proposal = GetById(id);

            if(proposal == null)
            {
                return false;
            }

            proposal.DescriptionOfProposal += description;
            proposal.Date = new DateTime();

            try
            {
                _context.Update(proposal);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        // heleper methods
        private void SetProposalsObjectsUpdate(ref RequestForCooperation newProposalForCooperation, Company company, Employee employee)
        {
            Company tempCompany = new Company();
            Employee tempEmployee = new Employee();

            if(string.IsNullOrEmpty(company.Name))
            {
                tempCompany = companyService.GetById(company.Id);
                _context.Entry(tempCompany).State = EntityState.Detached;
            }
            else
            {
                tempCompany = company;
            }
            if(string.IsNullOrEmpty(employee.FirstName))
            {
                tempEmployee = employeeService.GetById(employee.Id);
                _context.Entry(tempEmployee).State = EntityState.Detached;
            }
            else
            {
                tempEmployee = employee;
            }

            foreach(Location location in newProposalForCooperation.Company.Locations)
            {
                foreach(Location citiesLocations in tempCompany.Locations)
                {
                    if(citiesLocations.Id == location.Id)
                    {
                        citiesLocations.City = location.City;
                    }
                }
            }
           // _context.Entry(newProposalForCooperation.Company.Locations).State = EntityState.Detached;
            newProposalForCooperation.Company = tempCompany;
            newProposalForCooperation.Employee = tempEmployee;
            newProposalForCooperation.Date = DateTime.Now;
        }
        private bool CheckObjectsForUpdate(RequestForCooperation proposalForCooperation)
        {
            var company = companyService
               .GetById(proposalForCooperation.Company.Id);

            _context.Entry(company).State = EntityState.Detached;


            var employee = employeeService
                .GetById(proposalForCooperation.Employee.Id);

            _context.Entry(employee).State = EntityState.Detached;

            if (company == null || employee == null)
            {
                return false;
            }

            return true;
        }

        private void SetProposalsObjects(ref RequestForCooperation proposal, Company company, Employee employee)
        {
            proposal.Company = company;
            proposal.Employee = employee;
            proposal.Date = DateTime.Now;
        }

        private void SetProposalsAttributesUpdate(ref RequestForCooperation proposal, RequestForCooperation proposalForCooperation)
        {
            SetProposalsObjectsUpdate(ref proposal, proposalForCooperation.Company, proposalForCooperation.Employee);
            proposal.Title = proposalForCooperation.Title;
            proposal.DescriptionOfProposal = proposalForCooperation.DescriptionOfProposal;
            proposal.Date = DateTime.Now;
        }

        public RequestForCooperation GetLastInsterted()
        {
            return GetAll().LastOrDefault();
        }
    }
}
