using Anime.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Anime.Infraestrutura.Contexto
{
    public class Contexto : DbContext
    {
        public DbSet<Dominio.Entidades.Anime> Anime { get; set; }
        public DbSet<Diretor> Diretor { get; set; }

        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=AnimeDB;Uid=root;Pwd=", ServerVersion.AutoDetect("Server=localhost;Database=AnimeDB;Uid=root;Pwd="));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dominio.Entidades.Anime>(entity =>
            {
                entity.HasKey(x => x.Codigo);

                entity.Property(x => x.Nome)
                      .IsRequired();

                entity.HasOne(x => x.Diretor)
                      .WithMany(x => x.Animes)
                      .HasForeignKey(x => x.DiretorCodigo)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.Apagado)
                      .HasDefaultValue(false);
            });

            modelBuilder.Entity<Diretor>(entity =>
            {
                entity.HasKey(x => x.Codigo);

                entity.Property(x => x.Nome)
                      .IsRequired();
            });
        }
    }
}