using API.Model;

namespace API.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> AddAsync(T entity);
    Task<int> DeleteAsync(int id);
}

//TODO move these later
public interface IMemberRepository : IRepository<Member> { }

public interface IBookRepository : IRepository<Book>
{
    Task<bool> CheckoutAsync(int bookId, int memberId);
}

public interface ILaptopRepository : IRepository<Laptop>
{
    Task<bool> CheckoutAsync(int laptopId, int memberId);
}