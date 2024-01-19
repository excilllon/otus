using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Services;
using BtreeIndexProject.Services.BackgroundServices;
using BtreeIndexProject.Services.QueryExecution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSingleton<IMetaDataReader, MetaDataReader>();
builder.Services.AddTransient<IQueryExecutor, QueryExecutor>();
builder.Services.AddHostedService<DbmsInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();