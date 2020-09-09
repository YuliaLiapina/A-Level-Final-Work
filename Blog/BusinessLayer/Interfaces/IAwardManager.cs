using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IAwardManager
    {
        IList<AwardsModel> Get();
        AwardsModel Get(int id);
        void Add(AwardsModel award);
        void Update(AwardsModel award);
    }
}
