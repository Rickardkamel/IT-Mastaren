using System.Collections.Generic;
using Contracts;
using DataService.Repositories;
using Mappers;

namespace BusinessLogic.DataHandler
{
    public class AbsenceHandler
    {
        private AbsenceRepository absenceRepo = new AbsenceRepository();

        public List<AbsenceModel> GetAll()
        {
            return absenceRepo.GetAll().ToContracts();
        }

        public AbsenceModel Get(int id)
        {
            return absenceRepo.Get(id).ToContract();
        }

        public bool Post(AbsenceModel absence)
        {
            return absence != null && absenceRepo.Post(absence.ToDataBaseEntity());
        }

        public bool Delete(int id)
        {
            return absenceRepo.Delete(id);
        }
    }
}
