using DataAccess.Data;
using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class PairData : IPairData
{
    private readonly ISqlDataAccess _db;

    public PairData(ISqlDataAccess db)
    {
        _db = db;
    }
    public Task<IEnumerable<PairModel>> GetPairs() =>
        _db.LoadData<PairModel, dynamic>("dbo.spPair_GetAll", new { });

    public async Task<PairModel?> GetPair(int id)
    {
        var res = await _db.LoadData<PairModel, dynamic>("dbo.spPair_Get", new { Id = id });

        return res.FirstOrDefault();
    }

    public Task InsertPair(PairModel pair) =>
        _db.SaveData("dbo.spPair_Insert", new { pair.Title, pair.IdFirst, pair.IdSecond });


    public Task UpdatePair(PairModel pair) =>
        _db.SaveData("dbo.spPair_Update", new { pair.Id, pair.IdFirst, pair.IdSecond });

    public Task DeletePair(int id) =>
        _db.SaveData("dbo.spPair_Delete", new { Id = id });

    public async Task<PairModel?> GetAnotherIdById(int id)
    {
        var res = await _db.LoadData<PairModel, dynamic>("dbo.spPair_GetAnotherIdById", new { Id = id });

        return res.FirstOrDefault();
    }
}

