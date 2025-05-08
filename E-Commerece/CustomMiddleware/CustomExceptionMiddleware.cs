using System.Text.Json;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;

namespace E_Commerece.CustomMiddleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleware> logger;

        public CustomExceptionMiddleware(RequestDelegate Next,ILogger<CustomExceptionMiddleware> logger)
        {
            next = Next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);

                if(httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found"
                    };

                    //Return Object as JSON
                    var ResponseToReturn = JsonSerializer.Serialize(Response);
                    await httpContext.Response.WriteAsync(ResponseToReturn);
                }



            }
            catch (Exception ex)
            {
                //logger
                logger.LogError(ex, "Something Went Wrong");

                //Response Object
                var Response = new ErrorToReturn()
                {
                    ErrorMessage = ex.Message,

                };

                //Set Status Code For Response 
                Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException badRequestException => GetBadReqestErrors(badRequestException, Response),
                    _ => StatusCodes.Status500InternalServerError
                };
                //Set Content Type for Response 
                httpContext.Response.ContentType = "application/json";
                
                //Return Object as JSON

                var ResponseToReturn = JsonSerializer.Serialize(Response);

                await httpContext.Response.WriteAsync(ResponseToReturn);

            }

        }
        private static int GetBadReqestErrors(BadRequestException badRequestException,ErrorToReturn response)
        {
            response.Errors=badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

    }
}
