using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProjetoDti.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string Disciplina { get; set; }
        public decimal Valor { get; set; }

        // Relação com Aluno
        public Aluno Aluno { get; set; }
    }

    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Inicializa a coleção para evitar NullReferenceException
        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
        public double Nota1 { get; internal set; }
        public double Nota2 { get; internal set; }
        public double Nota3 { get; internal set; }
        public double Nota4 { get; internal set; }
        public double Nota5 { get; internal set; }
        public double MediaNotas { get; internal set; }
    }

    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais de modelo podem ser feitas aqui
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.Notas)
                .WithOne(n => n.Aluno)
                .HasForeignKey(n => n.AlunoId);
        }
    }
}
