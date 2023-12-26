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
            }
            else
            {
                _logger.LogWarning($"Visitor with id: {id} not found");
            }
        }

    }
}
