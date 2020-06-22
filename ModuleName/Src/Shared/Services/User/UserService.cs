using GOI.Seeker.Master.Entities;
using GOI.Seeker.Master.Shared.DatabaseContext;
using GOI.Seeker.Master.Shared.DTOs;
using GOI.Seeker.Master.Shared.Repositories;
using AutoMapper;
using GOI.Services.Common.Services;
using GOI.Services.Common.UnitOfWork;

namespace GOI.Seeker.Master.Shared.Services
{
    public class UserService : CrudService<User, UserDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IDatabaseUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository,
                           IDatabaseUnitOfWork unitOfWork,
                            IMapper mapper)
                : base(userRepository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

    }
}