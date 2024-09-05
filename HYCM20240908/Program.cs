using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/Account/login"; // Asegúrate de especificar el LoginPath si es necesario
        options.ReturnUrlParameter = "unauthorized"; // Corrige el error tipográfico
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var message = new
                {
                    error = "No autorizado",
                    message = "Debe iniciar sesión."
                };
                var jsonMessaje = JsonSerializer.Serialize(message);
                return context.Response.WriteAsync(jsonMessaje);
            }
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Debe ir antes de UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();
