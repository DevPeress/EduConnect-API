using EduConnect.Domain.Enums;

namespace EduConnect.ViewModels
{
    public class AtividadesDashBoardResponseViewModel
    {
        public required AuditAction Tipo { get; set; }
        public required string Dado { get; set; }
        public required int Horario { get; set; }
    }
}
