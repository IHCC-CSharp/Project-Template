using API.Data.Repositories;
using API.Model;

namespace API.Services;

public class MemberService(IMemberRepository memberRepo) : IMemberService
{
    public Task<IEnumerable<Member>> GetAllMembersAsync() => memberRepo.GetAllAsync();
    public Task<Member?> GetMemberByIdAsync(int id) => memberRepo.GetByIdAsync(id);
    public Task<int> AddMemberAsync(Member member) => memberRepo.AddAsync(member);
    public Task<int> DeleteMemberAsync(int id) => memberRepo.DeleteAsync(id);
}