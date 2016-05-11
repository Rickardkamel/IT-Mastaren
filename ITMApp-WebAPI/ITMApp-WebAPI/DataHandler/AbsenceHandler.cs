using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Contracts;
using ITMApp_WebAPI.Logic;
using ITMApp_WebAPI.Models;
using Microsoft.Ajax.Utilities;

namespace ITMApp_WebAPI.DataHandler
{
    class AbsenceHandler
    {
        private ITMAppContext _context;
        public AbsenceHandler(ITMAppContext context)
        {
            _context = context;
        }

        public List<AbsenceModel> GetAll()
        {
            return _context.Absences.ToList().ToContracts();
        }

        public AbsenceModel Get(int id)
        {
            return _context.Absences.FirstOrDefault(x => x.Id == id).ToContract();
        }

        public bool Post(AbsenceModel absence)
        {
            if (absence == null) return false;

            _context.Absences.AddOrUpdate(absence.ToDataBaseEntity());

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
