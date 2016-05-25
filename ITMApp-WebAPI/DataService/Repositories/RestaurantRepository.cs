using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataService.Repositories
{
    public class RestaurantRepository
    {
        private ITMAppContext _context = new ITMAppContext();

        public List<Restaurant> GetAll()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Id == id);
        }

        public bool Post(Restaurant restaurant)
        {
            if (restaurant == null) return false;

            _context.Restaurants.AddOrUpdate(restaurant);

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
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
            return true;
        }
    }
}