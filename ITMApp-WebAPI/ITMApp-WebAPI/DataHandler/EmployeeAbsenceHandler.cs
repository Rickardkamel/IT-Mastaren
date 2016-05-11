using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.WebPages;
using Contracts;
using ITMApp_WebAPI.Logic;
using ITMApp_WebAPI.Models;

namespace ITMApp_WebAPI.DataHandler
{
    public class EmployeeAbsenceHandler
    {
        private ITMAppContext _context;

        public EmployeeAbsenceHandler(ITMAppContext context)
        {
            _context = context;
        }

        public List<EmployeeAbsenceModel> GetAll()
        {
            var xx = _context.Employee_Absence.Where(x => x.Removed == false && (x.EndDate > DateTime.Now || x.EndDate == null)).ToList().ToContracts();

            return xx;
        }

        public EmployeeAbsenceModel Get(int id)
        {
            return _context.Employee_Absence.Where(x => x.Removed == false).FirstOrDefault(x => x.Id == id).ToContract();
        }

        public bool Post(EmployeeAbsenceModel employeeAbsence)
        {
            if (employeeAbsence == null) return false;

            _context.Employee_Absence.AddOrUpdate(employeeAbsence.ToDataBaseEntity());

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

            _context.Employee_Absence.AddOrUpdate(employeeAbsence);
            _context.SaveChanges();
            return true;
        }
    }
}
