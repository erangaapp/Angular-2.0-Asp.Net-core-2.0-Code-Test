using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using MD = CT.Models;

namespace CT.Data.Mappings
{
    public class PetMAP
    {
        public PetMAP(EntityTypeBuilder<MD.Pet> entityBuilder)
        {
            #region Keys

            entityBuilder.HasKey(t => t.Id);

            #endregion

            #region Properties

            entityBuilder.Property(t => t.Name).IsRequired().HasMaxLength(200);

            #endregion

            #region Table

            entityBuilder.ToTable("Pets");

            #endregion
        }
    }
}
