using System.Data;
using Dapper;

namespace API.Data.Repositories;

public abstract class BaseRepository<T>(IDbConnection db, string tableName) : IRepository<T> where T : class
{
    protected readonly IDbConnection _db = db;
    protected readonly string _tableName = tableName;

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _db.QueryAsync<T>($"SELECT * FROM {_tableName}");

    public async Task<T?> GetByIdAsync(int id) =>
        await _db.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @id", new { id });

    public async Task<int> DeleteAsync(int id) =>
        await _db.ExecuteAsync($"SELECT * FROM {_tableName} WHERE Id = @id", new { id });
    
    public abstract Task<int> AddAsync(T entity);
}