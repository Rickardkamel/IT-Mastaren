using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataService;
using DataService.Repositories;
using Mappers;

namespace BusinessLogic.DataHandler
{
    public class RestaurantHandler
    {
        private RestaurantRepository restaurantRepo = new RestaurantRepository();

        public List<RestaurantModel> GetAll()
        {
            return restaurantRepo.GetAll().ToContracts();
        }

        public RestaurantModel Get(int id)
        {
            return restaurantRepo.Get(id).ToContract();
        }

        public bool Post(RestaurantModel restaurant)
        {
            return restaurant != null && restaurantRepo.Post(restaurant.ToDataBaseEntity());
        }

        public bool Delete(int id)
        {
            return restaurantRepo.Delete(id);
        }
    }
}