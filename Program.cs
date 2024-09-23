using ProjetoDti.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona serviços ao container.
        builder.Services.AddControllersWithViews();

        // Configura o DbContext com a string de conexão correta
        IServiceCollection serviceCollection = builder.Services.AddDbContext<CustomDbContexto>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configura o pipeline de requisições HTTP.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // O valor padrão do HSTS é 30 dias. Pode ser alterado para cenários de produção.
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
