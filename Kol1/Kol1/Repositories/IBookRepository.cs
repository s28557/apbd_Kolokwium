using Kol1.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Kol1.Repositories;

public interface IBookRepository
{
    Task<bool> DoesBookExist(int id);
    Task<BookDTO> GetBook(int id);
    
    Task<int> AddNewBook(NewBookDTO book);
}