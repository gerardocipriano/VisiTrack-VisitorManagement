using System;
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

        public async Task<int> GetVisitorCount(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"GetVisitorCount called with startDate: {startDate}, endDate: {endDate}");
            var count = await _dbContext.Visitors
                .Where(v => v.TimestampEntrata >= startDate && v.TimestampEntrata <= endDate)
                .CountAsync();
            _logger.LogInformation($"Visitor count: {count}");
            return count;
        }

    }
}
