using AutoMapper;
using GOI.Seeker.Master.Entities;
using GOI.Seeker.Master.Shared.DTOs;
using GOI.Seeker.Master.Shared.Repositories;
using GOI.Services.Common.UnitOfWork;
using GOI.Services.Common.Services;

namespace GOI.Seeker.Master.Shared.Services
{
    public class EmployerService : CrudService<Employer, EmployerDTO>, IEmployerService
    {
        private readonly IMapper _mapper;
        private readonly IEmployerRepository _employerRepository;
        private readonly IDatabaseUnitOfWork _unitOfWork;
        public EmployerService(IEmployerRepository employerRepository,
                              IDatabaseUnitOfWork unitOfWork,
                             IMapper mapper) : base(employerRepository, unitOfWork, mapper)
        {

            _mapper = mapper;
            _employerRepository = employerRepository;
            _unitOfWork = unitOfWork;
        }
    }
}