using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GScores.Core.Domains.Entities;


// Base on rule, if student not attending the subject, the score will be 0;
public class Student
{
    [Key]
    public string? StudentId { get; set; }
    // These are required subjects that students must take
    [Column(TypeName = "decimal(3, 1)")]
    [DefaultValue(0.0)]
    [Range(0.0, 10.0)]
    public double? MathScore { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    [DefaultValue(0.0)]
    [Range(0.0, 10.0)]
    public double? LiteratureScore { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    [DefaultValue(0.0)]
    [Range(0.0, 10.0)]
    public double? ForeignScore { get; set; }

    [Required]
    [DefaultValue(true)]
    public bool? IsNaturalScience { get; set; } // Used to determine whether the student is in Natural Science or Social Science track

    // These column will null if student choose Natural Science
    [Column(TypeName = "decimal(4, 2)")]
    [Range(0.0, 10.0)]
    public double? HistoryScore { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    [Range(0.0, 10.0)]
    public double? GeographyScore { get; set; }
    [Column(TypeName = "decimal(4, 2)")]
    [Range(0.0, 10.0)]
    public double? CivicEducationScore { get; set; }

    // These column will null if student choose Social Science
    [Column(TypeName = "decimal(4, 2)")]
    [Range(0.0, 10.0)]
    public double? PhysicsScore { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    [Range(0.0, 10.0)]
    public double? ChemistryScore { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    [Range(0.0, 10.0)]
    public double? BiologyScore { get; set; }

    // Navigation properties
    public ForeignLanguageCode? ForeignLanguageCode { get; set; }
}