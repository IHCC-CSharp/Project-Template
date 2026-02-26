using API.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(IMemberService memberService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetAll() => 
        Ok(await memberService.GetAllMembersAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetById(int id)
    {
        var member = await memberService.GetMemberByIdAsync(id);
        return member == null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Member member)
    {
        await memberService.AddMemberAsync(member);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await memberService.DeleteMemberAsync(id);
        return NoContent();
    }
}