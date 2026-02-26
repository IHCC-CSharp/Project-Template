using System.Text.Json;
using API.Model;
using Dapper;
using Microsoft.Data.Sqlite;

namespace API.Data.Repositories;

public static class DbInitializer
{
    public static async Task Initialize(string connectionString)
    {
        await using var connection = new SqliteConnection(connectionString);
        connection.Open();

        await CreateTables(connection);

        // 2. Seed Data if empty
        var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Members");
        if (count != 0)
        {
            return;
        }
        var json = await File.ReadAllTextAsync("Data/SeedData.json"); 
        var data = JsonSerializer.Deserialize<SeedDataWrapper>(json);

        if (data == null)
        {
            return;
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            // Insert Members
            await connection.ExecuteAsync(
                "INSERT INTO Members (Id, Name, Email) VALUES (@Id, @Name, @Email)", 
                data.Members, transaction);

            // Insert Books
            await connection.ExecuteAsync(
                @"INSERT INTO Books (Id, Name, ItemCondition, Author, CurrentMemberId) 
                  VALUES (@Id, @Name, @ItemCondition, @Author, @CurrentMemberId)", 
                data.Books, transaction);

            // Insert Laptops
            await connection.ExecuteAsync(
                @"INSERT INTO Laptops (Id, Name, ItemCondition, SerialNumber, OS, CurrentMemberId) 
                  VALUES (@Id, @Name, @ItemCondition, @SerialNumber, @OS, @CurrentMemberId)", 
                data.Laptops, transaction);

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
    
    private static async Task CreateTables(SqliteConnection connection)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS Members (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Books (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                ItemCondition INTEGER NOT NULL,
                Author TEXT,
                CurrentMemberId INTEGER,
                FOREIGN KEY (CurrentMemberId) REFERENCES Members(Id)
            );

            CREATE TABLE IF NOT EXISTS Laptops (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                ItemCondition INTEGER NOT NULL,
                SerialNumber TEXT,
                OS TEXT,
                CurrentMemberId INTEGER,
                FOREIGN KEY (CurrentMemberId) REFERENCES Members(Id)
            );";

        await connection.ExecuteAsync(sql);
    }
    
    //TODO can i make this a record?
    private class SeedDataWrapper
    {
        public List<Member> Members { get; set; } = [];
        public List<Book> Books { get; set; } = [];
        public List<Laptop> Laptops { get; set; } = [];
    }
}