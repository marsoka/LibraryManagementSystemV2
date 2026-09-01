using FluentValidation;
using Library.Application.Exceptions;
using Library.Domain.Responses;
using System.Text.Json;

namespace Library.API.Middleware
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (AppException ex)
            {
                await HandleAppExceptionAsync(context, ex);
            }
            catch (Exception)
            {
                await HandleInternalServerErrorAsync(context);
            }
        }

        private static async Task HandleAppExceptionAsync(
            HttpContext context,
            AppException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var response = new ErrorResponse
            {
                Success = false,
                StatusCode = exception.StatusCode,
                Message = exception.Message,
                Errors = null
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleInternalServerErrorAsync(
            HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            var response = new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred.",
                Errors = null
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleValidationExceptionAsync(
            HttpContext context,
            ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorMessages = exception.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

            var response = new ErrorResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation failed.",
                Errors = errorMessages 
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
