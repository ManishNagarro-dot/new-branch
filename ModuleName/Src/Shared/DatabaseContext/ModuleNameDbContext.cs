using GOI.Seeker.Master.Entities;
using GOI.Services.Common.DatabaseContext;
using GOI.Services.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GOI.Seeker.Master.Shared.DatabaseContext
{
    /// <summary>
    /// DbContext class.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="IDatabaseUnitOfWork" />
    public class MasterDbContext : DbContextBase
    {

        public DbSet<Employer> Employers { get; set; }
        public DbSet<User> Users { get; set; }
                
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public MasterDbContext(DbContextOptions options) : base(options)
        {

        }

        
    }
}