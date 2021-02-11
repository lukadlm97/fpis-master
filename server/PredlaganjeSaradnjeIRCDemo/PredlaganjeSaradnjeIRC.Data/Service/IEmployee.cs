using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        Position PositionForEmployee(int employeeID);
    }
}
