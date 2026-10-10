using System;
using System.Collections.Generic;
using HeroesWeb.Models;
using HeroesWeb.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HeroesWeb.Data;

public partial class HeroesContext : DbContext
{
    public HeroesContext(DbContextOptions<HeroesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Heroes> Heroes { get; set; }

    public virtual DbSet<SuperPoderes> SuperPoderes { get; set; }

    // Nuevos DbSet
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Cuartel> Cuarteles { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Villano> Villanos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Heroes>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Heroes__3214EC0705FF0874");
        });

        modelBuilder.Entity<SuperPoderes>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__SuperPod__3214EC079350E7BE");

            entity.HasOne(d => d.Heroe)
                .WithMany(p => p.SuperPoderes)
                .HasConstraintName("FK_SuperPoderes_Heroes");
        });

        // Nuevas configuraciones
        modelBuilder.ApplyConfiguration(new CiudadConfiguration());
        modelBuilder.ApplyConfiguration(new VillanoConfiguration());

        modelBuilder.ApplyConfiguration(new CuartelConfiguration());
        modelBuilder.ApplyConfiguration(new VehiculoConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}