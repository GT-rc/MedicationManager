using MedicationManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        } 

        public DbSet<User> Users { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<MedSets> MedSets { get; set; }
        public DbSet<Set> Set { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<MedSets>().HasKey(c => new { c.UserId, c.SetId });
        }
        
        public ApplicationDbContext()
            : base()
        {

        }
        /*
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        } */
    }
}
