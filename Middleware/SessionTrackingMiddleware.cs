using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace E_commerceTechnologyWebsite.Middleware
{
    public class SessionTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public SessionTrackingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<SessionTrackingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation($"User {userId} accessed {context.Request.Path} at {DateTime.UtcNow}");

                // Cập nhật thời gian hoạt động cuối cùng
                context.Session.SetString("LastActivity", DateTime.UtcNow.ToString());

                // Kiểm tra thời gian không hoạt động
                var lastActivityString = context.Session.GetString("LastActivity");
                if (DateTime.TryParse(lastActivityString, out DateTime lastActivity))
                {
                    if (DateTime.UtcNow - lastActivity > TimeSpan.FromMinutes(30))
                    {
                        _logger.LogWarning($"User {userId} session expired due to inactivity");
                        await context.SignOutAsync();
                        context.Response.Redirect("/Account/Login");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
