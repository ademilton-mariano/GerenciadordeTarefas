using GerenciadorTarefas.Data.Mappings;
using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Data;

public class DataContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            "data source=ADEMILTON;initial Catalog=GerenciadroTarefasNovo;user id=sa;password=ademiltonM13;MultipleActiveResultSets=true;application Name=TesteD;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TarefaMap());
        modelBuilder.ApplyConfiguration(new UsuarioMap());
    }
}
