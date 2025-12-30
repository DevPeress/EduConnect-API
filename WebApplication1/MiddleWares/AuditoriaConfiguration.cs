using EduConnect.Application.Services;
using EduConnect.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EduConnect.MiddleWares;

public class AuditoriaConfiguration(AuditoriaService service)
{
    private readonly AuditoriaService _service = service;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();

        if (executedContext != null)
            return;

        var auditAttr = context.ActionDescriptor.EndpointMetadata
            .OfType<AuditAttribute>()
            .FirstOrDefault();

        if (auditAttr == null)
            return;

        var entityId = context.ActionArguments
            .FirstOrDefault().Value?.ToString() ?? "-";

        await _service.LogAsync(
            auditAttr.Action,
            auditAttr.Entity,
            entityId,
            "Ação executada com sucesso");
    }
}
