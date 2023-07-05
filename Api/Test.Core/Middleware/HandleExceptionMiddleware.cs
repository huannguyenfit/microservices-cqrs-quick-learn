
//using System.Net;
//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
//using Test.Core.Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//public class ExceptionHandlingMiddleware
//{
//    private const string JsonContentType = "application/json";
//    private readonly RequestDelegate _next;
//    private readonly IWebHostEnvironment _env;

//    public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
//    {
//        _next = next;
//        _env = env;
//    }

//    public async Task InvokeAsync(HttpContext httpContext)
//    {
//        try
//        {
//            await _next(httpContext);
//        }
//        catch (Exception ex)
//        {
//            await HandleExceptionAsync(httpContext, ex, _env);
//        }
//    }

//    private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
//    {
//        context.Response.ContentType = JsonContentType;
//        if (exception is ApiException ex)
//        {
//            // handle explicit 'known' API errors
//            //context.Exception = null;
//            context.Response.StatusCode = ex.StatusCode;

//            string jsonString = JsonConvert.SerializeObject(new ApiError(Message: ex.Message, Errors: ex.Errors, IsError: true));
//            return context.Response.WriteAsync(jsonString);
//        }
//        else if (exception is UnauthorizedAccessException)
//        {
//            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//            string jsonString = JsonConvert.SerializeObject(new ApiError(Message: "ApiError.Unauthorized"));
//            return context.Response.WriteAsync(jsonString);
//        }
//        else
//        {
//            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    
//                string jsonString = JsonConvert.SerializeObject( new ApiError(Message: exception.GetBaseException().Message));
//                return context.Response.WriteAsync(jsonString);
           
//        }
//    }

   
//}
