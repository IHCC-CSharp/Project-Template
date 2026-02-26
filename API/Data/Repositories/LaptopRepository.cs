using System.Data;
using API.Model;
using Dapper;

namespace API.Data.Repositories;

public class LaptopRepository(IDbConnection db) 
    : BaseRepository<Laptop>(db, "Laptops"), ILaptopRepository
{

    public override async Task<int> AddAsync(Laptop entity) =>
        await db.ExecuteAsync(@"INSERT INTO Laptops (Name, ItemCondition, SerialNumber, OS, CurrentMemberId) 
                                VALUES (@Name, @ItemCondition, @SerialNumber, @OS, @CurrentMemberId)", entity);
    
    public async Task<bool> CheckoutAsync(int bookId, int memberId)
    {
        // 1. Verify Member exists
        var memberExists = await db.ExecuteScalarAsync<bool>(
            "SELECT COUNT(1) FROM Members WHERE Id = @memberId", new { memberId });
        if (!memberExists) throw new KeyNotFoundException("Member not found.");

        // 2. Check if the book is already checked out
        var currentMember = await db.ExecuteScalarAsync<int?>(
            "SELECT CurrentMemberId FROM Books WHERE Id = @bookId", new { bookId });

        if (currentMember.HasValue)
            throw new InvalidOperationException("This book is already checked out by another member.");

        // 3. Perform Checkout
        var rowsAffected = await db.ExecuteAsync(
            "UPDATE Books SET CurrentMemberId = @memberId WHERE Id = @bookId", 
            new { memberId, bookId });

        return rowsAffected > 0;
    }

    public async Task<bool> ReturnAsync(int bookId)
    {
        var rowsAffected = await db.ExecuteAsync(
            "UPDATE Books SET CurrentMemberId = NULL WHERE Id = @bookId", 
            new { bookId });

        return rowsAffected > 0;
    }
}