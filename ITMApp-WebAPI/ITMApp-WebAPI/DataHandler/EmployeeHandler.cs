using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using Contracts;
using ITMApp_WebAPI.Logic;
using ITMApp_WebAPI.Models;

namespace ITMApp_WebAPI.DataHandler
{
    public class EmployeeHandler
    {
        private ITMAppContext _context;

        public EmployeeHandler(ITMAppContext context)
        {
            _context = context;
        }

        public EmployeeModel Get(int id)
        {
            var user = _context.Employees.FirstOrDefault(x => x.Id == id);

            return user?.ToContract();
        }

        public EmployeeModel GetUserName(string userName)
        {
            var user = _context.Employees.FirstOrDefault(x => x.UserName == userName);

            return user?.ToContract();
        }

        public List<EmployeeModel> GetAll()
        {
            return _context.Employees.ToList().ToContracts();
        }

        public bool Post(EmployeeModel employee)
        {
            if (employee == null) return false;

            _context.Employees.AddOrUpdate(employee.ToDataBaseEntity());

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
