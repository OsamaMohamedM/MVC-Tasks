namespace PipelineGuardian.Middleware
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MaintenanceMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var isMaintenanceMode = _configuration.GetValue<bool>("MaintenanceMode");
            if (isMaintenanceMode)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("The service is currently under maintenance. Please try again later.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}