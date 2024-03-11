using jexercise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
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

#region Company

apiGroup.MapGet("/companies", (JexContext ctx, CancellationToken cancellationToken) => ctx.Companies.ToListAsync(cancellationToken));
apiGroup.MapPut("/companies", async (JexContext ctx, [FromBody] Company company, CancellationToken cancellationToken) =>
{
    await ctx.Companies.AddAsync(company, cancellationToken);
    await ctx.SaveChangesAsync(cancellationToken);
});
apiGroup.MapPost("/companies/{id}", async (int id, JexContext ctx, [FromBody] Company newCompany, CancellationToken cancellationToken) =>
{
    var company = await ctx.Companies.Where(p => p.Id == id).SingleAsync(cancellationToken);

    company.Name = newCompany.Name;
    company.Address = newCompany.Address;

    await ctx.SaveChangesAsync(cancellationToken);
});
apiGroup.MapDelete("/companies/{id}", async (int id, JexContext ctx, CancellationToken cancellationToken) =>
{
    var company = await ctx.Companies.Where(p => p.Id == id).SingleAsync(cancellationToken);
    ctx.Companies.Remove(company);
    await ctx.SaveChangesAsync(cancellationToken);
});

#endregion

#region JobOffer

apiGroup.MapGet("/job-offers", (JexContext ctx, CancellationToken cancellationToken) => ctx.JobOffers.Include(p => p.Company).ToListAsync(cancellationToken));
apiGroup.MapPut("/job-offers", async (JexContext ctx, [FromBody] JobOffer jobOffer, CancellationToken cancellationToken) =>
{
    await ctx.JobOffers.AddAsync(jobOffer, cancellationToken);
    await ctx.SaveChangesAsync(cancellationToken);
});
apiGroup.MapPost("/job-offers/{id}", async (int id, JexContext ctx, [FromBody] JobOffer newJobOffer, CancellationToken cancellationToken) =>
{
    var jobOffer = await ctx.JobOffers.Where(p => p.Id == id).SingleAsync(cancellationToken);

    jobOffer.Title = newJobOffer.Title;
    jobOffer.Description = newJobOffer.Description;

    await ctx.SaveChangesAsync(cancellationToken);
});
apiGroup.MapDelete("/job-offers/{id}", async (int id, JexContext ctx, CancellationToken cancellationToken) =>
{
    var jobOffer = await ctx.JobOffers.Where(p => p.Id == id).SingleAsync(cancellationToken);
    ctx.JobOffers.Remove(jobOffer);
    await ctx.SaveChangesAsync(cancellationToken);
});

#endregion

using (var scope = app.Services.CreateScope())
{
    // Migrate on startup
    scope.ServiceProvider
        .GetRequiredService<JexContext>()
        .Database.Migrate();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();



