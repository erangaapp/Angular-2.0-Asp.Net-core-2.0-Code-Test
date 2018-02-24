using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using MD = CT.Models;

namespace CT.Data.Mappings
{
    public class PetTypeMAP
    {
        public PetTypeMAP(EntityTypeBuilder<MD.PetType> entityBuilder)
        {
            #region Keys

            entityBuilder.HasKey(t => t.Id);

            #endregion

            #region Properties

            entityBuilder.Property(t => t.Type).IsRequired().HasMaxLength(100);

            #endregion

            #region ForignKeys

            entityBuilder.HasMany(t => t.Pets).WithOne(t => t.PetType).HasForeignKey(t => t.PetTypeID);

            #endregion

            #region Table

            entityBuilder.ToTable("PetTypes");

            #endregion

        }
    }
}
