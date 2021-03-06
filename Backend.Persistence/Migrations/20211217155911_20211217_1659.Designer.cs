// <auto-generated />
using System;
using Backend.Persistence.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Data.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20211217155911_20211217_1659")]
    partial class _20211217_1659
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Backend.Data.Commande", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateEnregistrement")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NumeroCommande")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("Backend.Data.LigneCommande", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CommandeId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Prix")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid>("ProduitId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommandeId");

                    b.HasIndex("ProduitId");

                    b.ToTable("LigneCommandes");
                });

            modelBuilder.Entity("Backend.Data.Produit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateEnregistrement")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Libelle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Reference")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("Backend.Data.Propriete", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProduitId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TypeProprieteId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Valeur")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ProduitId");

                    b.HasIndex("TypeProprieteId");

                    b.ToTable("Proprietes");
                });

            modelBuilder.Entity("Backend.Data.TypePropriete", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("EstArchive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("TypeProprietes");
                });

            modelBuilder.Entity("Backend.Data.LigneCommande", b =>
                {
                    b.HasOne("Backend.Data.Commande", null)
                        .WithMany("LigneCommandes")
                        .HasForeignKey("CommandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Produit", null)
                        .WithMany("LigneCommandes")
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Propriete", b =>
                {
                    b.HasOne("Backend.Data.Produit", null)
                        .WithMany("Proprietes")
                        .HasForeignKey("ProduitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.TypePropriete", null)
                        .WithMany("Proprietes")
                        .HasForeignKey("TypeProprieteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Commande", b =>
                {
                    b.Navigation("LigneCommandes");
                });

            modelBuilder.Entity("Backend.Data.Produit", b =>
                {
                    b.Navigation("LigneCommandes");

                    b.Navigation("Proprietes");
                });

            modelBuilder.Entity("Backend.Data.TypePropriete", b =>
                {
                    b.Navigation("Proprietes");
                });
#pragma warning restore 612, 618
        }
    }
}
