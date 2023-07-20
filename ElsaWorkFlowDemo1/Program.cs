using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using ElsaWorkFlowDemo1.Activities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services
                .AddElsa(elsa => elsa
                 .UseEntityFrameworkPersistence(ef => ef.UseSqlServer(builder.Configuration.GetConnectionString("Workflow")))
                 .AddConsoleActivities()
                .AddActivity<CommandActivity>()

                    );
builder.Services.AddElsaApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToPage("/_Host");
});
app.Run();
