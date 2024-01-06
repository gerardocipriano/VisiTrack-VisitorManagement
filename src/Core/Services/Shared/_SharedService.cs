using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Services.Shared
{
    public partial class SharedService
    {
        private readonly VisitrackDbContext _dbContext;
        private readonly ILogger<SharedService> _logger;

        public SharedService(VisitrackDbContext dbContext, ILogger<SharedService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task LogDatabaseContent()
        {
            var visitors = await _dbContext.Visitors.ToListAsync();

            foreach (var visitor in visitors)
            {
                _logger.LogInformation($"ID: {visitor.Id}, Nome: {visitor.Nome}, Cognome: {visitor.Cognome}, Email: {visitor.Email}, Azienda: {visitor.Azienda}, Referente: {visitor.Referente}, TimestampEntrata: {visitor.TimestampEntrata}, TimestampUscita: {visitor.TimestampUscita}");
            }
        }

        public async Task<Dictionary<DayOfWeek, int>> GetVisitorCountPerDayOfWeek(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"GetVisitorCountPerDayOfWeek called with startDate: {startDate}, endDate: {endDate}");

            var visitors = await _dbContext.Visitors
                .Where(v => v.TimestampEntrata.Date >= startDate.Date && v.TimestampEntrata.Date <= endDate.Date)
                .ToListAsync();

            _logger.LogInformation($"Fetched {visitors.Count} visitors from the database");

            var visitorCountPerDayOfWeek = visitors
                .GroupBy(v => v.TimestampEntrata.DayOfWeek)
                .ToDictionary(g => g.Key, g => g.Count());

            _logger.LogInformation($"Calculated visitor count per day of week: {string.Join(", ", visitorCountPerDayOfWeek.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");

            return visitorCountPerDayOfWeek;
        }

        public async Task<List<Visitor>> GetVisitorsByDate(DateTime date)
        {
            _logger.LogInformation($"GetVisitorsByDate called with date: {date}");

            var visitors = await _dbContext.Visitors
                .Where(v => v.TimestampEntrata.Date == date.Date && v.TimestampUscita == null)
                .ToListAsync();

            _logger.LogInformation($"Fetched {visitors.Count} visitors from the database");

            return visitors;
        }

        public async Task CheckoutVisitor(Guid id)
        {
            _logger.LogInformation($"CheckoutVisitor called with id: {id}");

            var visitor = await _dbContext.Visitors.FindAsync(id);
            if (visitor != null)
            {
                visitor.TimestampUscita = DateTime.Now;
                _dbContext.Visitors.Update(visitor);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Updated checkout timestamp for visitor with id: {id}");

                // Update the daily visitor count
                var dailyVisitorCount = await GetTodayActualVisitorsCount(DateTime.Today);
                _logger.LogInformation($"Updated daily visitor count: {dailyVisitorCount}");
            }
            else
            {
                _logger.LogWarning($"Visitor with id: {id} not found");
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            _logger.LogInformation($"GetUserById called with id: {id}");

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with id: {id} not found");
            }

            return user;
        }

        public async Task SaveUser(User user)
        {
            if (user.Id == default)
            {
                _logger.LogInformation($"Adding new user with email: {user.Email}");

                // Set the Id to a new Guid
                user.Id = Guid.NewGuid();

                _dbContext.Users.Add(user);
            }
            else
            {
                _logger.LogInformation($"Updating user with id: {user.Id}");

                _dbContext.Users.Update(user);
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Saved user with id: {user.Id}");
        }

        public async Task<bool> DeleteUser(DeleteUserCommand cmd, string currentUserId)
        {
            if (cmd.Id == Guid.Parse(currentUserId))
            {
                return false;
            }

            var user = await _dbContext.Users
                .Where(x => x.Id == cmd.Id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<VisitorSelectDTO>> Search(string Ricerca)
        {
            _logger.LogInformation($"Search called with Ricerca: {Ricerca}");

            var queryable = _dbContext.Visitors.AsQueryable();

            // Get today's date at midnight
            var today = DateTime.Today;

            // Filter visitors who have TimestampUscita as null and TimestampEntrata as today's date
            queryable = queryable.Where(x => x.TimestampUscita == null && x.TimestampEntrata.Date == today);

            if (!string.IsNullOrWhiteSpace(Ricerca))
            {
                queryable = queryable.Where(x =>
                    x.Nome.Contains(Ricerca, StringComparison.OrdinalIgnoreCase) ||
                    x.Cognome.Contains(Ricerca, StringComparison.OrdinalIgnoreCase) ||
                    x.Email.Contains(Ricerca, StringComparison.OrdinalIgnoreCase) ||
                    x.Azienda.Contains(Ricerca, StringComparison.OrdinalIgnoreCase) ||
                    x.Referente.Contains(Ricerca, StringComparison.OrdinalIgnoreCase));
            }

            var visitors = await queryable
                .Select(x => new VisitorSelectDTO.Visitor
                {
                    Id = x.Id,
                    Email = x.Email,
                    Nome = x.Nome,
                    Cognome = x.Cognome,
                    Azienda = x.Azienda,
                    Referente = x.Referente,
                    TimestampEntrata = x.TimestampEntrata,
                    TimestampUscita = x.TimestampUscita
                })
                .ToListAsync();

            _logger.LogInformation($"Fetched {visitors.Count} visitors from the database");

            var visitorDTOs = visitors.Select(visitor => new VisitorSelectDTO
            {
                Visitors = new List<VisitorSelectDTO.Visitor> { visitor },
                Count = 1
            }).ToList();

            return visitorDTOs;
        }

        public async Task<int> GetTodayActualVisitorsCount(DateTime date)
        {
            _logger.LogInformation($"GetTodayActualVisitorsCount called with date: {date}");

            // Fetch all visitors who have checked in today and haven't checked out yet
            var visitors = await _dbContext.Visitors
                .Where(v => v.TimestampEntrata.Date == date.Date && v.TimestampUscita == null)
                .ToListAsync();

            // Return the count of these visitors
            return visitors.Count;
        }

    }
}
