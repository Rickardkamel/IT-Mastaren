using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataService.Repositories
{
    public class LunchRepository
    {
        private ITMAppContext _context = new ITMAppContext();
        public List<Lunch> GetAll()
        {
            //var nowPlusOne = DateTime.Now.AddHours(1);
            return _context.Lunches.Where(x => x.Removed == false).ToList();
            //&& (x.LunchTime < nowPlusOne)
        }

        public Lunch Get(int id)
        {
            return _context.Lunches.Where(x => x.Removed == false).FirstOrDefault(x => x.Id == id);
        }

        public bool Post(Lunch lunch, Employee employee)
        {
            var dbEmployee = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);

            if (lunch.Id == 0)
            {
                lunch.Employees = new List<Employee> {dbEmployee};
                _context.Lunches.AddOrUpdate(lunch);
                _context.SaveChanges();
            }

            var dbLunch = _context.Lunches?.FirstOrDefault(x => x.Id == lunch.Id);

            if (dbLunch != null && !dbLunch.Employees.Contains(dbEmployee))
            {
                dbLunch.Employees.Add(dbEmployee);
            }

            try
            {
                _context.Lunches.AddOrUpdate(dbLunch);

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
            var lunch = _context.Lunches.Find(id);
            if (lunch == null) return false;
            lunch.Removed = true;

            _context.Lunches.AddOrUpdate(lunch);
            _context.SaveChanges();
            return true;
        }
    }
}