using System.Data;
using API.Model;
using Dapper;

namespace API.Data.Repositories;

public class MemberRepository(IDbConnection db) : IMemberRepository
{
    public async Task<IEnumerable<Member>> GetAllAsync() =>
        await db.QueryAsync<Member>("SELECT * FROM Members");

    public async Task<Member?> GetByIdAsync(int id) =>
        await db.QueryFirstOrDefaultAsync<Member>("SELECT * FROM Members WHERE Id = @id", new { id });

    public async Task<int> AddAsync(Member entity) =>
        await db.ExecuteAsync("INSERT INTO Members (Name, Email) VALUES (@Name, @Email)", entity);

    public async Task<int> DeleteAsync(int id) =>
        await db.ExecuteAsync("DELETE FROM Members WHERE Id = @id", new { id });
}