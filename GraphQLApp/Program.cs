using GraphQL.Extensions;
using GraphQL.Schema;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UserType = GraphQL.Schema.UserType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGraphQlServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.UseGraphQLPlayground("/playground");

app.Run();