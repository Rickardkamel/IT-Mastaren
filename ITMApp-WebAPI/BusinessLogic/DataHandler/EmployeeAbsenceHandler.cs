using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.Repositories;
using Mappers;

namespace BusinessLogic.DataHandler
{
    public class EmployeeAbsenceHandler
    {
        private EmployeeAbsenceRepository employeeAbsenceRepo = new EmployeeAbsenceRepository();

        public List<EmployeeAbsenceModel> GetAll()
        {
            return employeeAbsenceRepo.GetAll().ToContracts();
        }

        public EmployeeAbsenceModel Get(int id)
        {
            return employeeAbsenceRepo.Get(id).ToContract();
        }

        public bool Post(EmployeeAbsenceModel employeeAbsence)
        {
            return employeeAbsence != null && employeeAbsenceRepo.Post(employeeAbsence.ToDataBaseEntity());
        }

        public bool Update(int id)
        {
            return employeeAbsenceRepo.Update(id);
        }
    }
}
