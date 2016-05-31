using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;

namespace Mappers
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

        public static RestaurantModel ToContract(this Restaurant restaurant)
        {
            if (restaurant == null) return null;
            return new RestaurantModel
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                ImagePath = restaurant.ImagePath
            };
        }

        public static List<RestaurantModel> ToContracts(this List<Restaurant> restaurants)
        {
            return restaurants?.ConvertAll(x => new RestaurantModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath
            });
        }

        public static Restaurant ToDataBaseEntity(this RestaurantModel restaurant)
        {
            return new Restaurant
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                ImagePath = restaurant.ImagePath
            };
        }

        public static List<Restaurant> ToDataBaseEntities(this List<RestaurantModel> restaurants)
        {
            return restaurants?.ConvertAll(x => new Restaurant
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath
            });
        }

        public static LunchModel ToContract(this Lunch lunch)
        {
            if (lunch == null) return null;
            return new LunchModel
            {
                Id = lunch.Id,
                EmployeesList = lunch.Employees.ToList().ToContracts(),
                LunchTime = lunch.LunchTime,
                Restaurant = lunch.Restaurant.ToContract(),
                RestaurantId = lunch.Restaurant.Id,
                Removed = lunch.Removed
            };
        }

        public static List<LunchModel> ToContracts(this List<Lunch> lunches)
        {
            return lunches?.ConvertAll(x => new LunchModel
            {
                Id = x.Id,
                EmployeesList = x.Employees.ToList().ToContracts(),
                LunchTime = x.LunchTime,
                Restaurant = x.Restaurant.ToContract(),
                RestaurantId = x.Restaurant.Id,
                Removed = x.Removed
            });
        }

        public static Lunch ToDataBaseEntity(this LunchModel lunch)
        {
            if (lunch == null) return null;
            return new Lunch
            {
                Id = lunch.Id,
                Employees = lunch.EmployeesList.ToDataBaseEntities(),
                LunchTime = lunch.LunchTime,
                Restaurant = lunch.Restaurant.ToDataBaseEntity(),
                RestaurantId = lunch.Restaurant.Id,
                Removed = lunch.Removed
            };
        }

        public static List<Lunch> ToDataBaseEntities(this List<LunchModel> lunches)
        {
            return lunches?.ConvertAll(x => new Lunch
            {
                Id = x.Id,
                Employees = x.EmployeesList.ToDataBaseEntities(),
                LunchTime = x.LunchTime,
                Restaurant = x.Restaurant.ToDataBaseEntity(),
                RestaurantId = x.Restaurant.Id,
                Removed = x.Removed
            });
        }
    }
}
