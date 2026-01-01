namespace EduConnect.Application.DTO.StoredProcedures;

public class GetTotalDashBoard
{
    public int TotalAlunos { get; set; }
    public int TotalProfessores { get; set; }
    public int TotalTurmas { get; set; }
    public int TotalPresencas { get; set; }
}