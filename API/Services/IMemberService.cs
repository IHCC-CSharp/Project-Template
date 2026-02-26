using API.Model;

namespace API.Services;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(int id);
    Task<int> AddMemberAsync(Member member);
    Task<int> DeleteMemberAsync(int id);
}