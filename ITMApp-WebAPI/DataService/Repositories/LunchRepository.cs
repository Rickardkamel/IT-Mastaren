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

        public bool Post(Lunch lunch)
        {
            if (lunch == null) return false;
            _context.Lunches.AddOrUpdate(lunch);

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
            var lunch = _context.Lunches.Find(id);
            if (lunch == null) return false;
            lunch.Removed = true;

            _context.Lunches.AddOrUpdate(lunch);
            _context.SaveChanges();
            return true;
        }
    }
}