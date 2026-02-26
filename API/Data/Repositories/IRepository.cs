using API.Model;

namespace API.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> AddAsync(T entity);
    Task<int> DeleteAsync(int id);
}

//TODO move these later to separate files maybe
public interface IMemberRepository : IRepository<Member> { }


public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAssetsAsync();
}


public interface IBookRepository : IRepository<Book>
{
    Task<bool> CheckoutAsync(int bookId, int memberId);
    Task<bool> ReturnAsync(int bookId);
}

public interface ILaptopRepository : IRepository<Laptop>
{
    Task<bool> CheckoutAsync(int laptopId, int memberId);
    Task<bool> ReturnAsync(int bookId);
}