using System.Data;
using API.Model;
using Dapper;

namespace API.Data.Repositories;

public class BookRepository(IDbConnection db) 
    : BaseRepository<Book>(db, "Books"), IBookRepository
{

    public override async Task<int> AddAsync(Book entity) =>
        await db.ExecuteAsync("""
                              INSERT INTO Books (Name, ItemCondition, Author, CurrentMemberId) 
                                                              VALUES (@Name, @ItemCondition, @Author, @CurrentMemberId)
                              """, entity);
    
    public async Task<bool> CheckoutAsync(int bookId, int memberId)
    {
        // 1. Verify Member exists
        var memberExists = await db.ExecuteScalarAsync<bool>(
            "SELECT COUNT(1) FROM Members WHERE Id = @memberId", new { memberId });
        
        if (!memberExists) 
            throw new KeyNotFoundException($"Member with ID {memberId} does not exist.");

        // 2. Check if the book is already checked out
        var currentMember = await db.ExecuteScalarAsync<int?>(
            "SELECT CurrentMemberId FROM Books WHERE Id = @bookId", new { bookId });

        if (currentMember.HasValue)
            throw new InvalidOperationException($"Book {bookId} is already checked out to member {currentMember}.");

        // 3. Update the record
        var rowsAffected = await db.ExecuteAsync(
            "UPDATE Books SET CurrentMemberId = @memberId WHERE Id = @bookId", 
            new { memberId, bookId });

        return rowsAffected > 0;
    }

    public async Task<bool> ReturnAsync(int bookId)
    {
        // Clear the CurrentMemberId by setting it to NULL
        var rowsAffected = await db.ExecuteAsync(
            "UPDATE Books SET CurrentMemberId = NULL WHERE Id = @bookId", 
            new { bookId });

        return rowsAffected > 0;
    }
}