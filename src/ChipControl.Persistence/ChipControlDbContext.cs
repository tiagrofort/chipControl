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
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
    public DbSet<Operadora> Operadoras => Set<Operadora>();
    public DbSet<Simcard> Simcards => Set<Simcard>();

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

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.ToTable("Funcionarios");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NomeCompleto).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Matricula).HasMaxLength(50);
            entity.Property(e => e.Setor).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cargo).HasMaxLength(100);
            entity.Property(e => e.TelefonePessoal).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Ativo).IsRequired();
            entity.Property(e => e.Observacoes).HasColumnType("text");
        });

        modelBuilder.Entity<Operadora>(entity =>
        {
            entity.ToTable("Operadoras");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Cnpj).HasMaxLength(20);
            entity.Property(e => e.Telefone).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Ativo).IsRequired();
            entity.Property(e => e.Observacoes).HasColumnType("text");
            entity.Property(e => e.DataCadastro).IsRequired();
        });

        modelBuilder.Entity<Simcard>(entity =>
        {
            entity.ToTable("Simcards");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.OperadoraId).IsRequired();
            entity.HasOne(e => e.Operadora)
                  .WithMany()
                  .HasForeignKey(e => e.OperadoraId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.IdentificacaoChip).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Iccid).IsRequired().HasMaxLength(22);
            entity.Property(e => e.Ddd).HasMaxLength(3);
            entity.Property(e => e.PlanoTipo).HasMaxLength(100);
            entity.Property(e => e.TemMinutagem).IsRequired();
            entity.Property(e => e.QuantidadeMinutos);
            entity.Property(e => e.TemInternet).IsRequired();
            entity.Property(e => e.QuantidadeInternet);
            entity.Property(e => e.DataAquisicao);
            entity.Property(e => e.DataAtivacao);
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Observacoes).HasColumnType("text");
            entity.Property(e => e.Ativo).IsRequired();
            entity.Property(e => e.DataCadastro).IsRequired();
            entity.Property(e => e.DataAlteracao);

            // Regras de duplicidade documentadas (modelo de dados, seção 4):
            // ICCID único; identificacao_chip único por operadora.
            entity.HasIndex(e => e.Iccid).IsUnique();
            entity.HasIndex(e => new { e.OperadoraId, e.IdentificacaoChip }).IsUnique();
        });
    }
}
