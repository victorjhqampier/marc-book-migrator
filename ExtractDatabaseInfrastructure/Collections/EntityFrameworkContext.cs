using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ExtractDatabaseInfrastructure.Collections;

public partial class EntityFrameworkContext : DbContext
{
    public EntityFrameworkContext()
    {
    }

    public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Axauthor> Axauthors { get; set; }

    public virtual DbSet<Axclasification> Axclasifications { get; set; }

    public virtual DbSet<Axcopy> Axcopies { get; set; }

    public virtual DbSet<Axpublisher> Axpublishers { get; set; }

    public virtual DbSet<Axserial> Axserials { get; set; }

    public virtual DbSet<Axtitle> Axtitles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf16_spanish_ci")
            .HasCharSet("utf16");

        modelBuilder.Entity<Axauthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("axauthors");

            entity.Property(e => e.CDate)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("cDate")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("cName")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CRole)
                .HasMaxLength(254)
                .HasColumnName("cRole")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CSurname)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("cSurname")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdAuthor)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idAuthor");
            entity.Property(e => e.IdTitle)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idTitle");
        });

        modelBuilder.Entity<Axclasification>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("axclasifications");

            entity.Property(e => e.CDescription)
                .HasColumnType("text")
                .HasColumnName("cDescription")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CDewey)
                .HasMaxLength(255)
                .HasColumnName("cDewey")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdClasification)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idClasification");
            entity.Property(e => e.IdTitle)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idTitle");
        });

        modelBuilder.Entity<Axcopy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("axcopies");

            entity.Property(e => e.CBarcode)
                .HasMaxLength(255)
                .HasColumnName("cBarcode")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CDocumentType)
                .HasMaxLength(255)
                .HasColumnName("cDocumentType")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CLocation)
                .HasMaxLength(255)
                .HasColumnName("cLocation")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CNotation)
                .HasMaxLength(255)
                .HasColumnName("cNotation")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CSection)
                .HasMaxLength(255)
                .HasColumnName("cSection")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CStatus)
                .HasMaxLength(255)
                .HasColumnName("cStatus")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdCopy)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idCopy");
            entity.Property(e => e.IdDocumentType)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(5) unsigned")
                .HasColumnName("idDocumentType");
            entity.Property(e => e.IdLocation)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("idLocation");
            entity.Property(e => e.IdSection)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("idSection");
            entity.Property(e => e.IdStatus)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("idStatus");
            entity.Property(e => e.IdTitle)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idTitle");
        });

        modelBuilder.Entity<Axpublisher>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("axpublishers");

            entity.Property(e => e.CName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("cName")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CPlace)
                .HasMaxLength(204)
                .HasColumnName("cPlace")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdPublisher)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idPublisher");
            entity.Property(e => e.IdTitle)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idTitle");
        });

        modelBuilder.Entity<Axserial>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("axserials");

            entity.Property(e => e.CNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("cNumber")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CTitle)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("cTitle")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdSerial)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idSerial");
            entity.Property(e => e.IdTitle)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idTitle");
        });

        modelBuilder.Entity<Axtitle>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("axtitle");

            entity.Property(e => e.CContent)
                .HasColumnType("mediumtext")
                .HasColumnName("cContent")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CDewey)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("cDewey")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CEdition)
                .HasMaxLength(255)
                .HasColumnName("cEdition")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CImage)
                .HasColumnName("cImage")
                .UseCollation("utf8mb4_uca1400_ai_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.CIsbn)
                .HasMaxLength(50)
                .HasColumnName("cIsbn")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CNotes)
                .HasColumnType("mediumtext")
                .HasColumnName("cNotes")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CPhysicaldesc)
                .HasColumnType("text")
                .HasColumnName("cPhysicaldesc")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CSubtitle)
                .HasColumnType("mediumtext")
                .HasColumnName("cSubtitle")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CTitle)
                .HasColumnType("mediumtext")
                .HasColumnName("cTitle")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CTopics)
                .HasColumnType("mediumtext")
                .HasColumnName("cTopics")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CType)
                .HasMaxLength(5)
                .HasDefaultValueSql("''")
                .HasColumnName("cType")
                .UseCollation("utf8mb4_uca1400_ai_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.IdTitle)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint(8) unsigned")
                .HasColumnName("idTitle");
            entity.Property(e => e.NReleased)
                .HasMaxLength(50)
                .HasColumnName("nReleased")
                .UseCollation("utf8mb3_unicode_ci")
                .HasCharSet("utf8mb3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
