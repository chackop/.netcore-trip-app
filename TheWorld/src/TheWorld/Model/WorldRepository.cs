using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TheWorld.Model
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var trip = GetTripByName(tripName, username);
            if (trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("getting all trips from datatabse");
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return _context.Trips
            .Include(t => t.Stops)
            .Where(t => t.Name == tripName && t.UserName == username)
            .FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
            .Include(t => t.Stops)
            .OrderBy(t => t.Name)
            .Where(t => t.UserName == name)
            .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Couldnt get trips with stops form DB", ex);
                return null;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
