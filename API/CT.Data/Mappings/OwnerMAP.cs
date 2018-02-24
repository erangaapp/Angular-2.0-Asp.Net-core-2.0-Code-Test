using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using MD = CT.Models;

namespace CT.Data.Mappings
{
    public class OwnerMAP
    {
        public OwnerMAP(EntityTypeBuilder<MD.Owner> entityBuilder)
        {
            #region Keys

            entityBuilder.HasKey(t => t.Id);

            #endregion

            #region Properties

            entityBuilder.Property(t => t.Name).IsRequired().HasMaxLength(200);

            #endregion

            #region ForignKeys

            entityBuilder.HasMany(t => t.Pets).WithOne(t => t.Owner).HasForeignKey(t => t.OwnerID);

            #endregion

            #region Table

            entityBuilder.ToTable("PetOwners");

            #endregion

        }
    }
}
