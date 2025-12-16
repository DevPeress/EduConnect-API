using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduConnect.Infra.CrossCutting.Entities;

public class CustomProblemDetails : ProblemDetails
{
    public object? Details { get; set; }

    public class ProblemDetailsFactory(IHttpContextAccessor accessor)
    {
        public IHttpContextAccessor? Accessor { get; } = accessor;

        public CustomProblemDetails Ok(object? result)
        {
            return new CustomProblemDetails
            {
                Status = (int)HttpStatusCode.OK,
                Details = result,
                Title = "OK",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.3.1",
                Instance = Accessor?.HttpContext?.Request?.Path
            };
        }

        public CustomProblemDetails BadRequest(object? result)
        {
            return new CustomProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Details = result,
                Title = "Bad Request",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                Instance = Accessor?.HttpContext?.Request?.Path
            };
        }
    }
}
