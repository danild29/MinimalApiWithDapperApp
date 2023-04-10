namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parametrs, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parametrs, string connectionId = "Default");
    }
}