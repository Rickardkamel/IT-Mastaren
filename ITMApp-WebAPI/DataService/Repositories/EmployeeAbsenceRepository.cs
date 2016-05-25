using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataService.Repositories
{
    public class EmployeeAbsenceRepository
    {
        private ITMAppContext _context = new ITMAppContext();

        public List<Employee_Absence> GetAll()
        {
            return _context.Employee_Absence.Where(x => x.Removed == false && (x.EndDate > DateTime.Now || x.EndDate == null)).ToList();
        }

        public Employee_Absence Get(int id)
        {
            return _context.Employee_Absence.Where(x => x.Removed == false).FirstOrDefault(x => x.Id == id);
        }

        public bool Post(Employee_Absence employeeAbsence)
        {
            if (employeeAbsence == null) return false;

            _context.Employee_Absence.AddOrUpdate(employeeAbsence);

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

        public bool Update(int id)
        {
            var employeeAbsence = _context.Employee_Absence.Find(id);
            
            if (employeeAbsence == null) return false;

            employeeAbsence.Removed = true;

            _context.Employee_Absence.AddOrUpdate(employeeAbsence as Employee_Absence);
            _context.SaveChanges();
            return true;
        }
    }
}
