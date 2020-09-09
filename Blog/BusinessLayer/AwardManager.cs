using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class AwardManager : IAwardManager
    {
        private readonly IRepository<Awards, int> _awardsRepo;
        private readonly IMapper _mapper;

        public AwardManager(IRepository<Awards, int> awardsRepository, IMapper mapper)
        {
            _awardsRepo = awardsRepository;
            _mapper = mapper;
        }

        public IList<AwardsModel> Get()
        {
            var award = _awardsRepo.Get();
            var usersModel = _mapper.Map<IList<AwardsModel>>(award);

            return usersModel;
        }

        public AwardsModel Get(int id)
        {
            var award = _awardsRepo.Get(id);
            var usersModel = _mapper.Map<AwardsModel>(award);

            return usersModel;
        }

        public void Add(AwardsModel award)
        {
            _awardsRepo.Add(_mapper.Map<Awards>(award), true);
        }

        public void Update(AwardsModel award)
        {
            var entity = _awardsRepo.Get(award.Id);

            entity.Name = award.Name;

            _awardsRepo.Update(entity, true);
        }
    }
}
