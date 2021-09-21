using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio
{
    class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Evento> Evento { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<Palestrante> Palestrante { get; set; }
        public DbSet<PalestranteEvento> PalestranteEvento { get; set; }
        public DbSet<RedeSocial> RedeSocials { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>().HasMany<Lote>(e => e.Lotes).WithOne(l => l.Evento).HasForeignKey(l => l.EventoId);
            modelBuilder.Entity<Evento>().HasMany<RedeSocial>(e => e.RedeSocias).WithOne(rs => rs.Evento).HasForeignKey(rs => rs.EventoId);
            modelBuilder.Entity<Evento>().HasMany<PalestranteEvento>(e => e.Palestrantes).WithOne(p => p.Evento).HasForeignKey(l => l.EventoId);
            modelBuilder.Entity<Palestrante>().HasMany<PalestranteEvento>(p => p.Eventos).WithOne(e => e.Palestrante).HasForeignKey(l => l.PalestranteId);
            modelBuilder.Entity<PalestranteEvento>().HasKey(k => new { k.EventoId, k.PalestranteId });
            modelBuilder.Entity<Palestrante>().HasMany<RedeSocial>(p => p.RedeSocias).WithOne(rs => rs.Palestrante).HasForeignKey(rs => rs.PalestranteId);



            base.OnModelCreating(modelBuilder);
        }

    }
}
