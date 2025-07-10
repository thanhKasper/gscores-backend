using System.ComponentModel.DataAnnotations;

namespace GScores.Core.Domains;

// Violate the 3NF if combined this into Student table
public class ForeignLanguageCode
{
    [Key]
    public string? ForeignCode { get; set; }
    public List<Student> Students { get; set; } = [];
}