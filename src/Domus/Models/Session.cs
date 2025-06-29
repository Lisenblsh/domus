using System.ComponentModel.DataAnnotations;

namespace Domus.Models;

public class Session
{
    [Key]
    public string Id { get; set; } = null!;
}
