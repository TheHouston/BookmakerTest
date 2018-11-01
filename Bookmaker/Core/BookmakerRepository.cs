using Bookmaker.BookmakerModel;
using Bookmaker.DbModel;
using Bookmaker.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmaker.Core
{
    public class BookmakerRepository : IBookmakerRepository
    {
        private readonly BookmakerContext _context;
        public BookmakerRepository(BookmakerContext context)
        {
            _context = context;
        }
       
        public string Authorization(string login, string password)
        {
            var player = _context.Players.FirstOrDefault(user => user.Name.Equals(login));
            if (player == null)
                throw new Exception("Login not found");
            var encryptedPassword = SecurePasswordHasher.Encrypt(password);
            if (encryptedPassword.Equals(player.Password))
                return JwtHelper.GenerateToken(player.PlayerId);
            throw new Exception("Auth failed");
        }

        public List<Events> GetActualEventsByChampId(int champId)
        {
            var events = _context.Events.Where(x => x.ChampId == champId && x.EventDate > DateTime.UtcNow);
            if (!events.Any())
                throw new Exception("Actual events for this championship not found");
            return events.ToList();
        }

        public List<Sport> GetActualSports()
        {
            var actualSports = new List<Sport>();
            var events = _context.Events.Where(x => x.EventDate > DateTime.UtcNow);
            if (!events.Any())
                throw new Exception("Actual events not found");
            foreach (var sport in _context.Sports)
            {                
                var champs = _context.Championships.Where(x => x.SportId == sport.SportId);
                var actualChamps = new List<Champ>();
                foreach (var champ in champs)
                {
                    var eventsByChamp = events.Where(x => x.ChampId == champ.ChampId);
                    if (eventsByChamp.Any())
                    {
                        var actualChamp = new Champ();
                        var actualEvents = new List<Event>();
                        foreach (var @event in eventsByChamp)
                        {
                            actualEvents.Add(new Event
                            {
                                EventId = @event.EventId,
                                ChampId = @event.ChampId,
                                HomeTeamId = @event.HomeTeamId,
                                AwayTeamId = @event.AwayTeamId,
                                AwayPrice = @event.AwayPrice,
                                DrawPrice = @event.DrawPrice,
                                EventDate = @event.EventDate,
                                HomePrice = @event.HomePrice
                            });
                        }
                        actualChamps.Add(new Champ
                        {
                            ChampId = champ.ChampId,
                            ChampName = champ.ChampName,
                            Events = actualEvents,
                            EventsQuantity = eventsByChamp.Count()
                        });
                    }                   
                }
                if (actualChamps.Any())
                {
                    actualSports.Add(new Sport
                    {
                        SportId = sport.SportId,
                        SportName = sport.SportName,
                        Champs = actualChamps,
                        EventsQuantity = actualChamps.Sum(x => x.EventsQuantity)
                    });
                }
            }
            return actualSports;
        }

        public int GetPaymentId(int playerId)
        {
            var player = _context.Players.FirstOrDefault(user => user.PlayerId == playerId);
            return player == null ?
                throw new Exception("Player not found") :
                player.PaymentId;
        }

        public int PlaceBet(int eventId, int playerId ,int teamId, double price, decimal betSum)
        {
            var actualEvent = _context.Events.FirstOrDefault(x => x.EventId == eventId);
            if (actualEvent == null)
                throw new Exception("Event not found");
            if (actualEvent.EventDate <= DateTime.UtcNow)
                throw new Exception("Event started");
            var bet = new Bets
            {
                EventId = eventId,
                PlayerId = playerId,
                Stake = betSum,
                Price = price,
                TeamId = teamId
            };
            _context.Bets.Add(bet);
            _context.SaveChanges();
            return bet.BetId;
        }
    }
}
