using API.Model;

namespace API.Services;

public interface IAssetService
{
    Task<IEnumerable<Asset>> GetAllAssetsAsync();
    
    
    // Book Operations
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<int> AddBookAsync(Book book);
    Task<int> DeleteBookAsync(int id);
    Task CheckoutBookAsync(int bookId, int memberId);
    Task ReturnBookAsync(int bookId);

    // Laptop Operations
    Task<IEnumerable<Laptop>> GetAllLaptopsAsync();
    Task<Laptop?> GetLaptopByIdAsync(int id);
    Task<int> AddLaptopAsync(Laptop laptop);
    Task<int> DeleteLaptopAsync(int id);
    Task CheckoutLaptopAsync(int laptopId, int memberId);
    Task ReturnLaptopAsync(int laptopId);
}