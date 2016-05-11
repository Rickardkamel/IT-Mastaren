using System.Collections.Generic;
using Contracts;
using ITMApp_WebAPI.Models;
using Microsoft.Ajax.Utilities;

namespace ITMApp_WebAPI.Logic
{
    public static class Mapper
    {
        public static EmployeeModel ToContract(this Employee employee)
        {
            
            return new EmployeeModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                UserName = employee.UserName
            };
        }

        public static List<EmployeeModel> ToContracts(this List<Employee> employees)
        {
            return employees?.ConvertAll(x => new EmployeeModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                UserName = x.UserName
            });
        }

        public static Employee ToDataBaseEntity(this EmployeeModel employee)
        {
            return new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                UserName = employee.UserName
            };
        }

        public static List<Employee> ToDataBaseEntities(this List<EmployeeModel> employees)
        {
            return employees?.ConvertAll(x => new Employee
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                UserName = x.UserName
            });
        }


        public static AbsenceModel ToContract(this Absence absence)
        {
            if (absence == null) return null;
            return new AbsenceModel
            {
                Id = absence.Id,
                Name = absence.AbsenceType
            };
        }

        public static List<AbsenceModel> ToContracts(this List<Absence> absences)
        {
            return absences?.ConvertAll(x => new AbsenceModel
            {
                Id = x.Id,
                Name = x.AbsenceType
            });
        }

        public static Absence ToDataBaseEntity(this AbsenceModel absence)
        {
            return new Absence
            {
                Id = absence.Id,
                AbsenceType = absence.Name
            };
        }

        public static List<Absence> ToDataBaseEntities(this List<AbsenceModel> absences)
        {
            return absences?.ConvertAll(x => new Absence
            {
                Id = x.Id,
                AbsenceType = x.Name
            });
        }

        public static EmployeeAbsenceModel ToContract(this Employee_Absence employeeAbsence)
        {
            if (employeeAbsence == null) return null;

                return new EmployeeAbsenceModel
                {
                    Id = employeeAbsence.Id,
                    StartDate = employeeAbsence.StartDate,
                    EndDate = employeeAbsence.EndDate,
                    Employee = employeeAbsence.Employee.ToContract(),
                    Absence = employeeAbsence.Absence.ToContract(),
                    Removed = employeeAbsence.Removed
                };
        }

        public static List<EmployeeAbsenceModel> ToContracts(this List<Employee_Absence> employeeAbsences)
        {
            return employeeAbsences?.ConvertAll(x => new EmployeeAbsenceModel
            {
                Id = x.Id,
                StartDate = x.StartDate.Date,
                EndDate = x.EndDate,
                Employee = x.Employee.ToContract(),
                Absence = x.Absence.ToContract(),
                Removed = x.Removed
            });
        }

        public static Employee_Absence ToDataBaseEntity(this EmployeeAbsenceModel employeeAbsence)
        {
            return new Employee_Absence
            {
                Id = employeeAbsence.Id,
                AbsenceId = employeeAbsence.Absence.Id,
                EmployeeId = employeeAbsence.Employee.Id,
                StartDate = employeeAbsence.StartDate,
                EndDate = employeeAbsence.EndDate,
                Removed = employeeAbsence.Removed
                //Employee = employeeAbsence.Employee.ToDataBaseEntity(),
                //Absence = employeeAbsence.Absence.ToDataBaseEntity()
            };
        }

        public static List<Employee_Absence> ToDataBaseEntities(this List<EmployeeAbsenceModel> employeeAbsences)
        {
            return employeeAbsences?.ConvertAll(x => new Employee_Absence
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Employee = x.Employee.ToDataBaseEntity(),
                Absence = x.Absence.ToDataBaseEntity(),
                Removed = x.Removed
            });
        }
    }
}
