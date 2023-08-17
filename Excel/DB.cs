using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<BaseGuid> Guid { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Excel;Username=postgres;Password=123123");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseGuid>().ToTable("BaseGuid");
        }
    }
    [Table("BaseGuid")]
    public class BaseGuid
    {
        [Key]
        public int Id { get; set; }
        [Column("Guid")]
        public string? Guid { get; set; }
        [Column("DESCRIPTION_TYPE_ID")]
        public int? DESCRIPTION_TYPE_ID { get; set; }
        [Column("CODE")]
        public int? CODE { get; set; }
        [Column("SHORT_NAME")]
        public string? SHORT_NAME { get; set; }
        [Column("NAME")]
        public string? NAME { get; set; }
        [Column("BEGIN_DATE")]
        public string? BEGIN_DATE { get; set; }
        [Column("END_DATE")]
        public string? END_DATE { get; set; } 
        [Column("NOTE")]
        public string? NOTE { get; set; }
    }
}
