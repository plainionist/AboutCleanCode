using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Athena.Backlog.Adapters;

namespace Athena.Backlog.IO;

public class ControllerExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.HttpContext.Response.ContentType = "application/json";

        if (context.Exception is NotFoundException _)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        } 
        else if (context.Exception is BadRequestException _)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        context.Result = new JsonResult(new
        {
            context.Exception.Message,
        });
    }
}