namespace ChipControl.Persistence;

using ChipControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ChipControlDbContext : DbContext
{
    public ChipControlDbContext(DbContextOptions<ChipControlDbContext> options)
        : base(options)
    {
    }

    public DbSet<UsuarioSistema> UsuariosSistema => Set<UsuarioSistema>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.ToTable("UsuariosSistema");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Login).IsUnique();
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Login).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SenhaHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.NivelAcesso)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Ativo).IsRequired();
            entity.Property(e => e.Observacoes).HasColumnType("text");
            entity.Property(e => e.DataCadastro).IsRequired();
            entity.Property(e => e.DataAlteracao);
        });
    }
}
