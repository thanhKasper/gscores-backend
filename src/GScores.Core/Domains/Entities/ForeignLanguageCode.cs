using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GScores.Core.Domains.Entities;

// Violate the 3NF if combined this into Student table
public class ForeignLanguageCode
{
    [Key]
    [Column(TypeName = "varchar(3)")]
    public string? ForeignCode { get; set; }
    public List<Student> Students { get; set; } = [];
}