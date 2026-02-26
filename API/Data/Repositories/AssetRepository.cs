using API.Model;
using Dapper;
using System.Data;

namespace API.Data.Repositories;

public class AssetRepository(IDbConnection db) : IAssetRepository
{
    public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
    {
        const string sql = @"
            SELECT Id, Name, ItemCondition, CurrentMemberId, Author, NULL as SerialNumber, NULL as OS, 'Book' as Type FROM Books
            UNION ALL
            SELECT Id, Name, ItemCondition, CurrentMemberId, NULL as Author, SerialNumber, OS, 'Laptop' as Type FROM Laptops";

        var rows = await db.QueryAsync<dynamic>(sql);

        return rows.Select(row =>
        {
            if (row.Type == "Book")
            {
                return new Book { 
                    Id = (int)row.Id, 
                    Name = row.Name, 
                    ItemCondition = (Condition)row.ItemCondition,
                    Author = row.Author,
                    CurrentMemberId = (int?)row.CurrentMemberId
                };
            }
            else
            {
                return (Asset)new Laptop { 
                    Id = (int)row.Id, 
                    Name = row.Name, 
                    ItemCondition = (Condition)row.ItemCondition,
                    SerialNumber = row.SerialNumber,
                    OS = row.OS,
                    CurrentMemberId = (int?)row.CurrentMemberId
                };
            }
        });
    }
}