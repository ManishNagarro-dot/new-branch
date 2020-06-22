using GOI.Seeker.Master.Entities;
using GOI.Seeker.Master.Shared.DatabaseContext;
using GOI.Services.Common.Repositories;
using GOI.Services.Common.UnitOfWork;

namespace GOI.Seeker.Master.Shared.Repositories
{
    /// <summary>
    /// The User repository class.
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The db context object.</param>
        public UserRepository(
            IDatabaseUnitOfWork dbContext)
            : base(dbContext) { }

    }
}