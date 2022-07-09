using MediatR;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CqrsDemoWebApi.Features.Books.Pipeline
{

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IWebHostEnvironment _environment;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string? requestData = null;

            if (_environment.IsDevelopment())
            {
                //log serializable payload data in request (as json format) to SeriLog if we are inside Development environment and the payload is below 50 kB in serializable size
                try
                {
                    requestData = request != null ? JsonConvert.SerializeObject(request) : null;
                    if (requestData?.Length > 50 * 1028)
                        requestData = null; //avoid showing data larger than 100 kB in serialized form in the logs of Seq / SeriLog                    
                }
                catch (Exception)
                {
                    //ignore 
                }
            }
            _logger.LogInformation($"Handling request {typeof(TRequest).Name} {{@requestData}}", requestData);
        
            var response = await next();

            string? responseData = null;
            if (_environment.IsDevelopment())
            {              
                //log serializable payload data in response (as json format) to SeriLog if we are in Development environment and the payload is below 50 kB in serialized size
                try
                {
                    responseData = response != null ? JsonConvert.SerializeObject(response) : null;
                    if (responseData?.Length > 50 * 1028)
                        responseData = null; //avoid showing data larger than 100 kB in serialized form in the logs of Seq / SeriLog
                }
                catch (Exception)
                {
                    //ignore 
                }
            }
            _logger.LogInformation($"Handled request {typeof(TRequest).Name}. Returning result of type {typeof(TResponse).Name} {{@responseData}}.", responseData);
            return response;
        }

    }

}
