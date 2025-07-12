using CsvHelper.Configuration;
using GScores.Infrastructure.ScoresRead.Interfaces;

namespace GScores.Infrastructure.ScoresRead;


public class StudentScoreMap : ClassMap<StudentScore>
{
    public StudentScoreMap()
    {
        Map(m => m.StudentId).Name("sbd");
        Map(m => m.MathScore).Name("toan");
        Map(m => m.LiteratureScore).Name("ngu_van");
        Map(m => m.ForeignScore).Name("ngoai_ngu");
        Map(m => m.PhysicsScore).Name("vat_li").Default(null as double?);
        Map(m => m.ChemistryScore).Name("hoa_hoc").Default(null as double?);
        Map(m => m.BiologyScore).Name("sinh_hoc").Default(null as double?);
        Map(m => m.HistoryScore).Name("lich_su").Default(null as double?);
        Map(m => m.GeographyScore).Name("dia_li").Default(null as double?);
        Map(m => m.CivicEducationScore).Name("gdcd").Default(null as double?);
        Map(m => m.ForeignCode).Name("ma_ngoai_ngu").Default(null);
    }
}