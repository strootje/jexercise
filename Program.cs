using jexercise;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<JexConfiguration>();
builder.Services.AddDbContext<JexContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var apiGroup = app.MapGroup("/api/v1");
apiGroup.MapGet("/companies", () => "Hello, WOrld!");

app.Run();
