using System.Net;
using System.Text.Json;

namespace test_library.Utils
{
    public class MiddlewareApiErrorHandler
    {
        private readonly ILogger _log;
        private readonly RequestDelegate _errorHandler;

        public MiddlewareApiErrorHandler(ILogger<MiddlewareApiErrorHandler> logger, RequestDelegate errorHandler)
        {
            _log = logger;
            _errorHandler = errorHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _errorHandler(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {

                    case CustomException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        _log.LogError(ex, ex.Message);
                        response.StatusCode = (int)(HttpStatusCode.InternalServerError);
                        break;
                }

                var result = JsonSerializer.Serialize(new { 
                    Message = ex.Message 
                });
                
                await response.WriteAsync(result);
            }

        }
    }
}
