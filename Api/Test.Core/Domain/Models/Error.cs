﻿//using System.Net;

//namespace Test.Core.Domain.Models
//{
//    public class ApiException : Exception
//    {
//        public int StatusCode { get; set; }

//        public List<ValidationError> Errors { get; set; }

//        public ApiException(string message, int statusCode = 500, List<ValidationError> errors = null) :
//            base(message)
//        {
//            StatusCode = statusCode;
//            Errors = errors;
//        }
//    }
//    public class ValidationApiException : ApiException
//    {
//        public ValidationApiException(ModelStateDictionary? modelState) :
//         base("Validation Failed", (int)HttpStatusCode.BadRequest)
//        {
//            Errors = modelState.Keys
//                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
//                .ToList();
//        }
//    }
//    public record struct ValidationError(string Field, string Message);
//    public record struct ApiError(string? Field = null, bool? IsError = null, string? Details = null, string? Message = null, List<ValidationError>? Errors = null);

//}