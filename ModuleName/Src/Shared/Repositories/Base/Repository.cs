using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using gov.seeker.moduleName.Entities;
using gov.seeker.moduleName.Shared.DatabaseContext;
using System.Security.Cryptography;

namespace gov.seeker.moduleName.Shared.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ModuleNameDbContext _dbcontext;
        private readonly DbSet<TEntity> _entities;

        public Repository(IDatabaseUnitOfWork dbcontext)
        {
            _dbcontext = (ModuleNameDbContext)dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _entities = _dbcontext.Set<TEntity>();
        }

        #region IRepository Implementation

        /// <summary>
        /// Get number of records exists in the database for this entity.
        /// </summary>
        /// <returns>Number of records in database</returns>
        public virtual int Count()
        {
            return _entities.Count();
        }

        /// <summary> 
        /// Get entity respective to the provided Id. 
        /// </summary>
        /// <param name="id"> Id of entity to be retreived. </param>
        /// <returns> Entity without associations. </returns>
        public virtual async Task<TEntity> Find(object id)
        {
            Guid.TryParse(id.ToString(), out Guid identifier);
            return await _entities.FindAsync(identifier);
        }



        /// <summary>
        /// Insert new entity in respective Dataset. 
        /// </summary>
        /// <param name="entity"> The entity to be insert.</param>
        /// <returns> an updated entity with Id.</returns>
        public virtual TEntity Add(TEntity entity)
        {
            _entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// Attach entity in respective data set.
        /// </summary>
        /// <param name="entityToUpdate">Entity To Update</param>
        public virtual void Attach(TEntity entityToUpdate)
        {
            _entities.Attach(entityToUpdate);
        }

        /// <summary>
        /// Update entity in respective dataset.
        /// </summary>
        /// <param name="entityToUpdate"> Entity To Update. </param>
        public virtual void Update(TEntity entityToUpdate)
        {
            Attach(entityToUpdate);
            _dbcontext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete the entity by Id.
        /// </summary>
        /// <param name="id"> The identifier for entity to be delete. </param>
        public virtual async Task Delete(object id)
        {
            TEntity entityToDelete = await Find(id);
            Delete(entityToDelete);
        }

        /// <summary> 
        /// Delete the entity.
        /// </summary>
        /// <param name="entityToDelete"> Entity To Delete. </param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbcontext.Entry(entityToDelete).State == EntityState.Detached)
            {
                Attach(entityToDelete);
            }

            _entities.Remove(entityToDelete);
        }

        /// <summary> 
        /// Delete the list of entity.
        /// </summary>
        /// <param name="entityListToDelete"> List of entities to be deleted. </param>
        public virtual void Delete(List<TEntity> entityListToDelete)
        {
            foreach (var entityToDelete in entityListToDelete)
            {
                if (_dbcontext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    Attach(entityToDelete);
                }
            }

            _entities.RemoveRange(entityListToDelete);
        }


        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>An enumerator that allows foreach to be used to process the entities in this collection.</returns>
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        #endregion IRepository Implementation
    }
}