using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataService.Repositories
{
    
    public class EmployeeRepository
    {
        private ITMAppContext _context = new ITMAppContext();

        public Employee Get(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public Employee GetUserName(string userName)
        {
            return _context.Employees.FirstOrDefault(x => x.UserName == userName);
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public bool Post(Employee employee)
        {
            if (employee == null) return false;
            if (_context.Employees.Any(x => x.UserName.ToLower() == employee.UserName.ToLower()))
            {
                return true;
            }

            _context.Employees.AddOrUpdate(employee);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null) return false;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }


    }
}
