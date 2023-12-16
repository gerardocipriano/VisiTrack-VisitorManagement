namespace Core.Services.Shared
{
    public partial class SharedService
    {
        VisitrackDbContext _dbContext;

        public SharedService(VisitrackDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
