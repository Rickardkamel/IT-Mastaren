using System;
using System.Collections.Generic;
using Contracts;
using DataService;
using DataService.Repositories;
using Mappers;

namespace BusinessLogic.DataHandler
{
    public class LunchHandler
    {
        private LunchRepository lunchRepo = new LunchRepository();

        public List<LunchModel> GetAll()
        {
            return lunchRepo.GetAll().ToContracts();
        }

        public LunchModel Get(int id)
        {
            return lunchRepo.Get(id).ToContract();
        }

        public bool Post(LunchModel lunch, EmployeeModel employee)
        {
            return lunch != null && lunchRepo.Post(lunch.ToDataBaseEntity(), employee.ToDataBaseEntity());
        }

        public bool Update(int id)
        {
            return lunchRepo.Update(id);
        }
    }
}