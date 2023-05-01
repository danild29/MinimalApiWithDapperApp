

using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    [MinLength(1)]
    public string FirstName { get; set; }
    [MinLength(1)]
    public string LastName { get; set; }
}

