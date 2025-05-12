using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ORMFoy5.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Fakulte> Fakulteler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<OgrenciDers> OgrenciDersler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Ogrenci>().ToTable("Ogrenci");
            modelBuilder.Entity<Bolum>().ToTable("Bolum");
            modelBuilder.Entity<Fakulte>().ToTable("Fakulte");
            modelBuilder.Entity<Ders>().ToTable("Ders");
            modelBuilder.Entity<OgrenciDers>().ToTable("OgrenciDers");

            modelBuilder.Entity<Ogrenci>()
                .HasRequired(o => o.Bolum)
                .WithMany(b => b.Ogrenciler)
                .HasForeignKey(o => o.BolumID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bolum>()
                .HasRequired(b => b.Fakulte)
                .WithMany(f => f.Bolumler)
                .HasForeignKey(b => b.FakulteID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ders>()
                .HasRequired(d => d.Bolum)
                .WithMany(b => b.Dersler)
                .HasForeignKey(d => d.BolumID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OgrenciDers>()
                .HasRequired(od => od.Ogrenci)
                .WithMany(o => o.OgrenciDersleri)
                .HasForeignKey(od => od.OgrenciID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OgrenciDers>()
                .HasRequired(od => od.Ders)
                .WithMany(d => d.OgrenciDersleri)
                .HasForeignKey(od => od.DersID)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}