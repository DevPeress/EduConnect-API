using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public enum AuditAction
{
    Create = 1,
    Update = 2,
    Delete = 3,
    Login = 4,
    Logout = 5,
    AccessDenied = 6
}


public class Registro
{
    [Key]
    public int Id { get; set; }

    // Usuário que realizou a ação
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserRole { get; set; } = null!;

    // O que foi executado
    public AuditAction Action { get; set; }
    public string Entity { get; set; } = null!;
    public string EntityId { get; set; } = null!;

    // Detalhes da ação
    public string Detalhes { get; set; } = null!;

    // Informações do usuario
    public DateTime CreatedAt { get; set; }
    public string IpAddress { get; set; } = null!;

    public bool Deletado { get; set; } = false;
}
