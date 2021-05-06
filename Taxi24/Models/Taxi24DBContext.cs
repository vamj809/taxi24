using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Taxi24.Models
{
    public partial class Taxi24DBContext : DbContext
    {
        public Taxi24DBContext()
        {
        }

        public Taxi24DBContext(DbContextOptions<Taxi24DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conductor> Conductores { get; set; }
        public virtual DbSet<Pasajero> Pasajeros { get; set; }
        public virtual DbSet<Viaje> Viajes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=queenie.db.elephantsql.com;Port=5432;Database=qoszeacd;Username=qoszeacd;Password=kQ5H0agkhj-Y-w3NDfGIibkCNJJ_-qEm");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("btree_gin")
                .HasPostgresExtension("btree_gist")
                .HasPostgresExtension("citext")
                .HasPostgresExtension("cube")
                .HasPostgresExtension("dblink")
                .HasPostgresExtension("dict_int")
                .HasPostgresExtension("dict_xsyn")
                .HasPostgresExtension("earthdistance")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("hstore")
                .HasPostgresExtension("intarray")
                .HasPostgresExtension("ltree")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("pgrowlocks")
                .HasPostgresExtension("pgstattuple")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Conductor>(entity =>
            {
                entity.ToTable("Conductores", "taxi24db");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Disponible)
                    .IsRequired()
                    .HasDefaultValueSql("true");
            });

            modelBuilder.Entity<Pasajero>(entity =>
            {
                entity.ToTable("Pasajeros", "taxi24db");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.ToTable("Viajes", "taxi24db");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ConductorId).HasColumnName("ConductorID");

                entity.Property(e => e.PasajeroId).HasColumnName("PasajeroID");

                entity.HasOne(d => d.Conductor)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.ConductorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ConductorViajes");

                entity.HasOne(d => d.Pasajero)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.PasajeroId)
                    .HasConstraintName("PasajeroViajes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
