using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceOne.Domain.ViewModels;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FinanceOne.WebApi.Middlewares
{
  public class GlobalExceptionHandlerMiddleware : IMiddleware
  {
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
      ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (BusinessException businessException)
      {
        await HandleBusinessExceptionAsync(context, businessException);
      }
      catch (Exception exception)
      {
        _logger.LogError($"Unexpected error: {exception}");
        await HandleInternalExceptionAsync(context, exception);
      }
    }

    private static Task HandleInternalExceptionAsync(
      HttpContext context,
      Exception exception
    )
    {
      const int statusCode = StatusCodes.Status500InternalServerError;

      var response = new ApiResponse<object>
      {
        Success = false,
        Errors = new List<string>() { "Internal Server Error." },
      };

      var json = JsonConvert.SerializeObject(response);

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(json);
    }

    private static Task HandleBusinessExceptionAsync(
      HttpContext context,
      BusinessException businessException
    )
    {
      const int statusCode = StatusCodes.Status400BadRequest;

      var response = new ApiResponse<object>
      {
        Success = false,
        Errors = businessException.BrokenRules.ToList(),
      };

      var json = JsonConvert.SerializeObject(response);

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(json);
    }
  }
}
