namespace Kol1.Models.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> genres { get; set; }
}

public class AuthorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class GenresDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}