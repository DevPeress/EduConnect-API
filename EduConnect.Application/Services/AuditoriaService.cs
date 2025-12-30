using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using EduConnect.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace EduConnect.Application.Services;

public class AuditoriaService(IAuditRepository repo, IHttpContextAccessor httpContext)
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IAuditRepository _auditRepository = repo;

    public async Task LogAsync(Registro registro)
    {
        await _auditRepository.AddAsync(registro);
    }
}
