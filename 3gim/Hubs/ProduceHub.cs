using _3gim.Data;
using _3gim.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace _3gim.Hubs
{
    public class ProduceHub : Hub
    {
        private readonly _3gimDbContext _dbContext;

        public ProduceHub(_3gimDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Supplies()
        {
            var result=_dbContext.Supplies.ToList();
            if (result.Count() == 0)
            {
                Supplies supplies = new Supplies();
                supplies.Quantity = 1;

                _dbContext.Supplies.Add(supplies);
                _dbContext.SaveChanges();
            }
            else
            {
                var result2 = _dbContext.Supplies.First();
                result2.Quantity += 1;
                _dbContext.SaveChanges();
            }
        }

        public void Defects()
        {
            var result = _dbContext.Defects.ToList();
            if (result.Count() == 0)
            {
                Defects defects = new Defects();
                defects.Quantity = 1;

                _dbContext.Defects.Add(defects);
                _dbContext.SaveChanges();
            }
            else
            {
                var result2 = _dbContext.Defects.First();
                result2.Quantity += 1;
                _dbContext.SaveChanges();
            }
        }
    }
}
