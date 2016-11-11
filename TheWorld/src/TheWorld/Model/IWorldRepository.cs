using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TheWorld.Model
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName, string username);
        void AddStop(string tripName, Stop newStop, string username);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}