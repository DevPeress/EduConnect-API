using EduConnect.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EduConnect.Application.Common.Auditing;

public class AuditContext : IAuditContext
{
    private readonly IHttpContextAccessor _http;

    public bool IsAuthenticated =>
        _http.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public string UserId =>
        _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? "Sistema";

    public string UserName =>
        _http.HttpContext?.User.Identity?.Name
        ?? "Sistema";

    public string UserRole =>
        _http.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value
        ?? "N/A";

    public string IpAddress =>
        _http.HttpContext?.Connection.RemoteIpAddress?.ToString()
        ?? "-";
}
