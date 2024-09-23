using ProjetoDti.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona servi�os ao container.
        builder.Services.AddControllersWithViews();

        // Configura o DbContext com a string de conex�o correta
        IServiceCollection serviceCollection = builder.Services.AddDbContext<CustomDbContexto>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configura o pipeline de requisi��es HTTP.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // O valor padr�o do HSTS � 30 dias. Pode ser alterado para cen�rios de produ��o.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
