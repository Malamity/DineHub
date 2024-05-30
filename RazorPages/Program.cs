using Application;
using Infrastructure.Extensions;
using Infrastructure.Middleware;
using GraphQL.Extensions;

namespace RazorPagesApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        builder.Services.AddRazorPages();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddGraphQlServices();

        builder.Services
            .AddJwtAuthentication(builder.Configuration)
            .AddApplication()
            .AddInfrastructure();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<CustomHeaderMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseGraphQLPlayground("/playground");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapGraphQL();
        });

        app.Run();
    }
}