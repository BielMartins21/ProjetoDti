using Microsoft.EntityFrameworkCore;
using ProjetoDti.Models;

namespace ProjetoDti.Models
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
        }

        // Definição do DbSet para Produto
        public DbSet<Produto> Produtos { get; set; }

        // Definição do DbSet para Aluno
        public DbSet<Aluno> Alunos { get; set; }

        // Outras tabelas podem ser adicionadas aqui conforme necessário

        // Método para configurar o modelo, se necessário
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais para suas entidades podem ser feitas aqui
        }
    }
}
