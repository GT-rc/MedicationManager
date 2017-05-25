/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MedicationManager.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicationManager.Data
{
    public class ApplicationContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source = ?");  // I don't understand this line...

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
*/