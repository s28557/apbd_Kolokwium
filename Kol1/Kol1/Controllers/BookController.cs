using Kol1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kol1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet("{id}/genres")]
    public async Task<IActionResult> GetBook(int id)
    {
        if (!await _bookRepository.DoesBookExist(id))
            return NotFound($"Book with given ID - {id} doesn't exist");

        var animal = await _bookRepository.GetBook(id);
            
        return Ok(animal);
    }
    
}