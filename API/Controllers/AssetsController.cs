using API.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController(IAssetService assetService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
    {
        return Ok(await assetService.GetAllAssetsAsync());
    }

    // --- BOOKS ---
    [HttpGet("books")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks() =>
        Ok(await assetService.GetAllBooksAsync());

    [HttpPost("books/checkout/{id}/{memberId}")]
    public async Task<ActionResult> CheckoutBook(int id, int memberId)
    {
        try
        {
            await assetService.CheckoutBookAsync(id, memberId);
            return Ok(new { message = "Book checked out successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("books/return/{id}")]
    public async Task<ActionResult> ReturnBook(int id)
    {
        await assetService.ReturnBookAsync(id);
        return Ok(new { message = "Book returned." });
    }

    [HttpPost("books")]
    public async Task<ActionResult> CreateBook(Book book)
    {
        await assetService.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpDelete("books/{id}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        await assetService.DeleteBookAsync(id);
        return NoContent();
    }


    // --- LAPTOPS ---
    [HttpPost("laptops")]
    public async Task<ActionResult> CreateLaptop(Laptop laptop)
    {
        await assetService.AddLaptopAsync(laptop);
        return CreatedAtAction(nameof(GetLaptops), new { id = laptop.Id }, laptop);
    }

    [HttpDelete("laptops/{id}")]
    public async Task<ActionResult> DeleteLaptop(int id)
    {
        await assetService.DeleteLaptopAsync(id);
        return NoContent();
    }


    [HttpGet("laptops")]
    public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops() =>
        Ok(await assetService.GetAllLaptopsAsync());

    [HttpPost("laptops/checkout/{id}/{memberId}")]
    public async Task<ActionResult> CheckoutLaptop(int id, int memberId)
    {
        try
        {
            await assetService.CheckoutLaptopAsync(id, memberId);
            return Ok(new { message = "Laptop checked out successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("laptops/return/{id}")]
    public async Task<ActionResult> ReturnLaptop(int id)
    {
        await assetService.ReturnLaptopAsync(id);
        return Ok(new { message = "Laptop returned." });
    }
}