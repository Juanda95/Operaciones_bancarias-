﻿using BankOperations.Application.Helpers.Exceptions;
using BankOperations.Application.Helpers.Wrappers;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BankOperations.Presentation.Handlers
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlerMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);

            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Messages = error?.Message };
                switch (error)
                {
                    case ApiException e:
                        //custon application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;                  
                    case KeyNotFoundException e:
                        //not fount error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // auditoria
                        // unhandled error
                        _logger.LogError(error.ToString());
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }

        }
    }
}
