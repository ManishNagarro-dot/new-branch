using System;
using System.Threading.Tasks;
using AutoMapper;
using gov.seeker.moduleName.Entities;
using gov.seeker.moduleName.Shared.DTOs;
using gov.seeker.moduleName.Shared.Repositories;
using gov.seeker.moduleName.Shared.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace gov.seeker.moduleName.Shared.Services
{
    public class CrudService<TEntity, TEntityDTO> : ICrudService<TEntityDTO>
        where TEntity : IEntityBase
        where TEntityDTO : IBaseDTO
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IDatabaseUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudService{TEntity, TEntityDTO}" /> class.
        /// Initialize itself with external dependencies required
        /// </summary>
        /// <param name="repository">Repository class for entities</param>
        /// <param name="unitOfWork">Context for entities where we expose only commit method</param>
        /// <param name="mapper">The mapper.</param>
        public CrudService(IRepository<TEntity> repository, IDatabaseUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Generic method to fetch entity from database and covert it to respective DTO
        /// </summary>
        /// <param name="id">Id of the entity to be fetched</param>
        /// <returns>DTO object respective to id provided </returns>
        public virtual async Task<TEntityDTO> Find(object id)
        {

            TEntity entity = await _repository.Find(id);
            TEntityDTO entityDTO = _mapper.Map<TEntity, TEntityDTO>(entity);
            return entityDTO;
        }

        public async Task<IEnumerable<TEntityDTO>> GetAll()
        {
            IEnumerable<TEntity> entities = await _repository.GetAll();
            IEnumerable<TEntityDTO> dataList = 
                _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return dataList;
        }

        /// <summary>
        /// This method is used to insert entities in database.
        /// </summary>
        /// <param name="entityDTO">entity DTO needs to be insert in database.</param>
        /// <returns>
        /// Returns entityDTO with success and failure information, 
        /// In case data validation failure it will throw an exception
        /// </returns>
        public async Task<TEntityDTO> Add(TEntityDTO entityDTO)
        {
            // We will map entity DTO into entity using auto mapper
            TEntity entity = _mapper.Map<TEntityDTO, TEntity>(entityDTO);
            entity = _repository.Add(entity);

            // Commit Changes to database
            await _unitOfWork.Commit();

            // Convert entities to DTO and return to the client application
            entityDTO = _mapper.Map<TEntity, TEntityDTO>(entity);
            return entityDTO;
        }

        /// <summary>
        /// This method is used to update existing entities in database.
        /// </summary>
        /// <param name="entityDTO"> Entity DTO needs to be update</param>
        /// <returns>Returns entityDTO with success and failure information, 
        /// In case data validation failure it will throw an exception</returns>
        public async Task<TEntityDTO> Update(TEntityDTO entityDTO)
        {
            // We will map entity DTO into entity using auto mapper
            TEntity entity = _mapper.Map<TEntityDTO, TEntity>(entityDTO);
            _repository.Update(entity);

            // Commit Changes to database
            await _unitOfWork.Commit();

            // Convert entities to DTO and return to the client application
            entityDTO = _mapper.Map<TEntity, TEntityDTO>(entity);
            return entityDTO;
        }

        /// <summary>
        /// This method is used to delete entities from the system
        /// </summary>
        /// <param name="id">id of entity to be delete.</param>
        public async Task Delete(object Id)
        {

            await _repository.Delete(Id);

            // Commit Changes to database
            await _unitOfWork.Commit();
        }

        /// <summary>
        /// This method is used to Save (Insert/Update) the collection of entity DTOs. 
        /// </summary>
        /// <param name="entityDTOs">The collection of entity DTOs.</param>
        /// <returns>Entity DTO Collection.</returns>
        public async Task<IEnumerable<TEntityDTO>> BulkInsert(IEnumerable<TEntityDTO> entityDTOs)
        {
            IList<TEntity> entities = new List<TEntity>();

            foreach (TEntityDTO entityDTO in entityDTOs)
            {
                TEntity entity = SaveEntity(entityDTO);
                entities.Add(entity);
            }

            //// Commit Changes to database
            await _unitOfWork.Commit();

            //// We will map entity into entity DTO using auto mapper
            return entities.Select(mapperEntity => _mapper.Map<TEntity, TEntityDTO>(mapperEntity));
        }

        #region Private Methods

        /// <summary>
        /// This method is used to save(insert/update) entity in database
        /// </summary>
        /// <param name="entityDTO">Entity DTO needs to be save</param>
        /// <returns>Return entity after insert/update of entityDTO</returns>
        private TEntity SaveEntity(TEntityDTO entityDTO)
        {
            TEntity entityMapper;

            if (entityDTO.Id != null)
            {
                //// We will map entity DTO into entity using auto mapper
                entityMapper = _mapper.Map<TEntityDTO, TEntity>(entityDTO);
                _repository.Add(entityMapper);
            }
            else
            {
                //// We will map entity DTO into entity using auto mapper
                entityMapper = _mapper.Map<TEntityDTO, TEntity>(entityDTO);
                _repository.Update(entityMapper);
            }

            return entityMapper;
        }

        #endregion Private Methods

    }
}