using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gov.seeker.moduleName.Entities;
using gov.seeker.moduleName.Shared.DatabaseContext;

namespace gov.seeker.moduleName.Shared.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntityBase
    {
        /// <summary>
        /// Get number of records exists in the database for this entity.
        /// </summary>
        /// <returns>Number of records in database</returns>
        int Count();

        /// <summary>
        /// Get entity respective to the provided Id.
        /// </summary>
        /// <param name="id">Id of entity to be retreived.</param>
        /// <returns>
        /// Entity without associations.
        /// </returns>
        Task<TEntity> Find(object id);

        /// <summary>Get all entities</summary>
        /// <returns> An enumerator that allows foreach to be used to process the entities in this
        /// collection. </returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Insert new entity in respective dataset.
        /// </summary>
        /// <param name="entity">The entity to be insert.</param>
        /// <returns>
        /// an updated entity with Id.
        /// </returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Attach entity in respective data set.
        /// </summary>
        /// <param name="entityToUpdate">Entity To Update.</param>
        void Attach(TEntity entityToUpdate);

        /// <summary>
        /// Update entity in respective dataset.
        /// </summary>
        /// <param name="entityToUpdate">Entity To Update.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Delete the entity by Id.
        /// </summary>
        /// <param name="id">The identifier for entity to be delete.</param>
        Task Delete(object id);

        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entityToDelete">Entity To Delete.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Delete the list of entity.
        /// </summary>
        /// <param name="entityListToDelete">List of entities to be deleted.</param>
        void Delete(List<TEntity> entityListToDelete);
    }
}