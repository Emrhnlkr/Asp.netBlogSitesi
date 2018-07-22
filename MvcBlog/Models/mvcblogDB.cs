namespace MvcBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mvcblogDB : DbContext
    {
        public mvcblogDB()
            : base("name=mvcblogDB")
        {
        }

        public virtual DbSet<Etiket> Etikets { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Makale> Makales { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Uye> Uyes { get; set; }
        public virtual DbSet<Yetki> Yetkis { get; set; }
        public virtual DbSet<Yorum> Yorums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etiket>()
                .HasMany(e => e.Makales)
                .WithMany(e => e.Etikets)
                .Map(m => m.ToTable("MakaleEtiket1").MapLeftKey("EtiketId").MapRightKey("MakaleId"));

            modelBuilder.Entity<Makale>()
                .HasMany(e => e.Yorums)
                .WithOptional(e => e.Makale)
                .HasForeignKey(e => e.MakaleId);

            modelBuilder.Entity<Makale>()
                .HasMany(e => e.Yorums1)
                .WithOptional(e => e.Makale1)
                .HasForeignKey(e => e.UyeId);

            modelBuilder.Entity<Yetki>()
                .HasMany(e => e.Uyes)
                .WithRequired(e => e.Yetki)
                .WillCascadeOnDelete(false);
        }
    }
}
