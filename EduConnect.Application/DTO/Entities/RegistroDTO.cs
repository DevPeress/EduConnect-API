namespace EduConnect.Application.DTO.Entities;
public record RegistroDTO
{
    public string UserName { get; set; } = null!;
    public string UserRole { get; set; } = null!;
    public string Action { get; set; } = null!;
    public string Entity { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
