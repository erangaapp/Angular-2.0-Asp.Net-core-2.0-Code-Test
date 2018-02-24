using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using MD = CT.Models;
using MAP = CT.Data.Mappings;

namespace CT.Data
{
    public class CTDbContext : DbContext
    {
        public CTDbContext(DbContextOptions<CTDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MD.Owner> Owners { get; set; }
        public virtual DbSet<MD.PetType> PetTypes { get; set; }
        public virtual DbSet<MD.Pet> Pets { get; set; }
        public virtual DbSet<MD.EventLog> EventLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region MAPPINGS

            new MAP.OwnerMAP(builder.Entity<MD.Owner>());
            new MAP.PetMAP(builder.Entity<MD.Pet>());
            new MAP.PetTypeMAP(builder.Entity<MD.PetType>());
            new MAP.EventLogMAP(builder.Entity<MD.EventLog>());

            #endregion

        }
    }
}
