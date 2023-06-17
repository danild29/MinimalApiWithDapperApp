

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

