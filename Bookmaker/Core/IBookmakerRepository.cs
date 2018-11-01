using Bookmaker.BookmakerModel;
using Bookmaker.DbModel;
using System.Collections.Generic;

namespace Bookmaker.Core
{
    public interface IBookmakerRepository
    {
        string Authorization(string login, string password);
        int GetPaymentId(int playerId);
        List<Sport> GetActualSports();
        List<Events> GetActualEventsByChampId(int champId);        
        int PlaceBet(int eventId, int playerId, int teamId, double price, decimal betSum);        
    }
}
