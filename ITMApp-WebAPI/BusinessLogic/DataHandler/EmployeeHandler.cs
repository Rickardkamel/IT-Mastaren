using System;
using System.Collections.Generic;
using Contracts;
using DataService;
using DataService.Repositories;
using Mappers;

namespace BusinessLogic.DataHandler
{
    public class EmployeeHandler
    {
        private EmployeeRepository employeeRepo = new EmployeeRepository();
        public EmployeeModel Get(int id)
        {
            return employeeRepo.Get(id).ToContract();
        }

        public EmployeeModel GetUserName(string userName)
        {
            return employeeRepo.GetUserName(userName).ToContract();
        }

        public List<EmployeeModel> GetAll()
        {
            return employeeRepo.GetAll().ToContracts();
        }

        public bool Post(EmployeeModel employee)
        {
            return employee != null && employeeRepo.Post(employee.ToDataBaseEntity());
        }

        public bool Delete(int id)
        {
            return employeeRepo.Delete(id);
        }


    }
}
