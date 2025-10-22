﻿using Jay.Workflow.WebApi.Common.Exceptions.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jay.Workflow.WebApi.Service.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context,ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            var errorOutput = new ErrorOutput { ErrorCode = InternalErrorCode.InternalServerError, Status = "INTERNAL_SERVER_ERROR" };
            
            if(exception is ApiBaseException apiBaseException)
            {
                errorOutput.ErrorCode= apiBaseException.ErrorCode;
                errorOutput.Status= apiBaseException.Status;
                errorOutput.Message= apiBaseException.Message;
                errorOutput.Detail= apiBaseException.Detail;

                await WriteResponseAsync(context, exception, errorOutput, apiBaseException.HttpCode).ConfigureAwait(false);
            }
            else
            {
                errorOutput.Message = "内部服务错误。";

                await WriteResponseAsync(context, exception, errorOutput).ConfigureAwait(false);
            }
        }

        private async Task WriteResponseAsync(HttpContext context,Exception exception,ErrorOutput errorOutput,int statusCode = StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, errorOutput.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var jsonSerializerSetting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorOutput, jsonSerializerSetting)).ConfigureAwait(false);
        }
    }
}
