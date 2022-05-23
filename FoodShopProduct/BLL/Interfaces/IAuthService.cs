using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<bool> IsTokenValid(string token);
    }
}