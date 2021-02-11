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
    public class EmployeeService : IEmployee
    {
        private readonly ApplicationContext _context;

        public EmployeeService(ApplicationContext context)
        {
            _context = context;
        }
        
        //main info about employee
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }
        public Employee GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(employee => employee.Id == id);
        }
        
        //bonus functionality
        public Position PositionForEmployee(int employeeId)
        {
            var atPosition = AtPostition(employeeId);

            if (atPosition == null)
            {
                return null;
            }
            
            return atPosition.Position;
        }
        private AtPosition AtPostition(int employeeId)
        {
            return _context.AtPostions
                .Include(pos => pos.Employee)
                .Include(pos => pos.Position)
                .FirstOrDefault(pos => pos.Employee.Id == employeeId);
        }
    }
}
