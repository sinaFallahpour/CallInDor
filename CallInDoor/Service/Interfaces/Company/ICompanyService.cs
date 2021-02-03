using Domain.Entities;
using System.Threading.Tasks;

namespace Service.Interfaces.Company
{
    public interface ICompanyService
    {
        public Task<bool> GetCompanyServiceUserTBL(string userId, int serviceTBLId);
    }
}
