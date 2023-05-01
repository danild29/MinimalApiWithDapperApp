using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IPairData
    {
        Task DeletePair(int id);
        Task<PairModel?> GetPair(int id);
        Task<IEnumerable<PairModel>> GetPairs();
        Task InsertPair(PairModel pair);
        Task UpdatePair(PairModel pair);
        Task<PairModel?> GetAnotherIdById(int id);
    }
}