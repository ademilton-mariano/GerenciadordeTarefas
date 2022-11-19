using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorTarefas.Data.Mappings;

public class TarefaMap : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("Tarefa");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(x => x.Descricao).IsRequired().HasMaxLength(255).HasColumnType("VARCHAR");
        builder.Property(x => x.Data).IsRequired().HasColumnType("VARCHAR").HasMaxLength(10);
        builder.Property(x => x.Hora).IsRequired().HasColumnType("VARCHAR").HasMaxLength(10);

        // Relacionamentos
        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Tarefas)
            .OnDelete(DeleteBehavior.Cascade);
    }
}