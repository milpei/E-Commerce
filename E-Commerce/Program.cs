using Microsoft.EntityFrameworkCore;
using E_Commerce.Context;
using E_Commerce.Repositories;
using E_Commerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Esto lo Agregamos para que el proyecto pueda usar la conexion a la base de datos, que use sql server y le pasamos la cadena de conexion que esta en el appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlString"));
});

builder.Services.AddScoped(typeof(GenericRepository<>));//typeof porque le puede llegar cualquier tipo de clase
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
