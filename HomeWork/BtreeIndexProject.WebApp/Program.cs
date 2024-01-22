using BtreeIndexProject.Abstractions.Indexing;
using BtreeIndexProject.Abstractions.MetaData;
using BtreeIndexProject.Abstractions.QueryExecution;
using BtreeIndexProject.Services.BackgroundServices;
using BtreeIndexProject.Services.Indexing;
using BtreeIndexProject.Services.MetaData;
using BtreeIndexProject.Services.QueryExecution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSingleton<IMetaDataManager, MetaDataManager>();
builder.Services.AddTransient<IQueryExecutor, QueryExecutor>();
builder.Services.AddTransient<IIndexWriter, IndexManager>();
builder.Services.AddTransient<IIndexReader, IndexManager>();
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