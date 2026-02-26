using API.Data.Repositories;
using API.Model;

namespace API.Services;

public class AssetService(IBookRepository bookRepo, ILaptopRepository laptopRepo, IAssetRepository assetRepository) : IAssetService
{
    public Task<IEnumerable<Asset>> GetAllAssetsAsync() => assetRepository.GetAllAssetsAsync();
    
    
    // --- Book Operations ---
    public Task<IEnumerable<Book>> GetAllBooksAsync() => bookRepo.GetAllAsync();
    public Task<Book?> GetBookByIdAsync(int id) => bookRepo.GetByIdAsync(id);
    public Task<int> AddBookAsync(Book book) => bookRepo.AddAsync(book);
    public Task<int> DeleteBookAsync(int id) => bookRepo.DeleteAsync(id);
    
    public async Task CheckoutBookAsync(int bookId, int memberId) 
        => await bookRepo.CheckoutAsync(bookId, memberId);
    
    public async Task ReturnBookAsync(int bookId) 
        => await bookRepo.ReturnAsync(bookId);

    // --- Laptop Operations ---
    public Task<IEnumerable<Laptop>> GetAllLaptopsAsync() => laptopRepo.GetAllAsync();
    public Task<Laptop?> GetLaptopByIdAsync(int id) => laptopRepo.GetByIdAsync(id);
    public Task<int> AddLaptopAsync(Laptop laptop) => laptopRepo.AddAsync(laptop);
    public Task<int> DeleteLaptopAsync(int id) => laptopRepo.DeleteAsync(id);

    public async Task CheckoutLaptopAsync(int laptopId, int memberId) 
        => await laptopRepo.CheckoutAsync(laptopId, memberId);

    public async Task ReturnLaptopAsync(int laptopId) 
        => await laptopRepo.ReturnAsync(laptopId);
}