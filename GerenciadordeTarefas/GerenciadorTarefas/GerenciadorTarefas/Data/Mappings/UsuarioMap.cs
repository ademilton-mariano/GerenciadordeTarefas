using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorTarefas.Data.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(80);
            builder.Property(x => x.Bio).IsRequired(false);
            builder.Property(x => x.Email).IsRequired().HasColumnName("Email").HasColumnType("VARCHAR").HasMaxLength(100);
            builder.Property(x => x.Endereco).IsRequired(false);
            builder.Property(x => x.Senha).IsRequired().HasColumnName("Senha").HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(x => x.Idade).IsRequired().HasColumnName("Idade").HasColumnType("INT");
        }
}