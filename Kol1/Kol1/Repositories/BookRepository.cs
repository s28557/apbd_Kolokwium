using Microsoft.Data.SqlClient;
using Kol1.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kol1.Repositories;

public class BookRepository
{
    private readonly IConfiguration _configuration;

    public BookRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesBookExist(int id)
    {
        var query = "SELECT 1 FROM books WHERE ID = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<BookDTO> GetBook(int id)
    {
        var query = @"SELECT 
                        Books.ID AS BookId, 
                        Books.Title AS BookTitle
                      FROM Books
                      WHERE BookId = @ID;";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
	    
        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();
        var bookIdOrdinal = reader.GetOrdinal("BookId");
        var bookTitleOrdinal = reader.GetOrdinal("BookTitle");

        var bookDTO = new BookDTO()
        {
            Id = reader.GetInt32(bookIdOrdinal),
            Title = reader.GetString(bookTitleOrdinal)
        };
        return bookDTO;
    }

    public async Task<int> AddNewBook(NewBookDTO book)
    {
        var insert = @"INSERT INTO Books VALUES(@Title, @Genre);
					   SELECT @@IDENTITY AS ID;";
	    
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
	    
        command.Connection = connection;
        command.CommandText = insert;

        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Gerne", book.Genre);
        
        await connection.OpenAsync();
	    
        var id = await command.ExecuteScalarAsync();

        if (id is null) throw new Exception();
	    
        return Convert.ToInt32(id);
    }
    
    
    
}