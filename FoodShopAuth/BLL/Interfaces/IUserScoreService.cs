using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserScoreService
    {
        Task<float> GetUserScore(Guid id);
        Task<IEnumerable<float>> GetUsersScore(IEnumerable<Guid> ids);
        Task<float> UpdateUserScore(Guid id);
    }
}
