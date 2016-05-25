using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataService.Repositories
{
    public class AbsenceRepository
    {
        private ITMAppContext _context = new ITMAppContext();

        public List<Absence> GetAll()
        {
            return _context.Absences.ToList();
        }

        public Absence Get(int id)
        {
            return _context.Absences.FirstOrDefault(x => x.Id == id);
        }

        public bool Post(Absence absence)
        {
            if (absence == null) return false;

            _context.Absences.AddOrUpdate(absence);

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
            var absence = _context.Absences.Find(id);

            if (absence == null) return false;

            _context.Absences.Remove(absence);
            _context.SaveChanges();
            return true;
        }
    }
}
