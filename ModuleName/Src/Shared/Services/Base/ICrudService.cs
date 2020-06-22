using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gov.seeker.moduleName.Entities;
using gov.seeker.moduleName.Shared.DTOs;

namespace gov.seeker.moduleName.Shared.Services
{
    public interface ICrudService<TEntityDTO>
        where TEntityDTO : IBaseDTO
    {
        /// <summary>
        /// Get the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The EntityDTO respective to identifier supplied. </returns>
        Task<TEntityDTO> Find(object id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Entities list</returns>
        Task<IEnumerable<TEntityDTO>> GetAll();

        /// <summary>
        /// Insert entity if all the business validations are passed.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>Updated entity</returns>
        Task<TEntityDTO> Add(TEntityDTO entityDTO);

        /// <summary>
        /// Update entity if all the business validations are passed..
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>Updated entity</returns>
        Task<TEntityDTO> Update(TEntityDTO entityDTO);

        /// <summary>
        /// Delete entity by updating the IsDeleted flag in the database.
        /// </summary>
        /// <param name="id">id of entity to be delete.</param>
        Task Delete(object id);

        /// <summary>
        /// Save (Insert/Update) the collection of entity dtos.
        /// </summary>
        /// <param name="entityDTOs">The collection of entity dtos </param>
        /// <returns> Collection of updated entities.</returns>
        Task<IEnumerable<TEntityDTO>> BulkInsert(IEnumerable<TEntityDTO> entityDTOs);
    }
}