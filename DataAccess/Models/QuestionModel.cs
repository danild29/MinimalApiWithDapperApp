using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class QuestionModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Question { get; set; }
    [Required]
    public string? Answer { get; set; }
    public byte[]? Content { get; set; }
    public string? ContentType { get; set; }
}
