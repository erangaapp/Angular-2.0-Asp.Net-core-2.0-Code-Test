using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using MD = CT.Models;

namespace CT.Data.Mappings
{
    public class EventLogMAP
    {
        public EventLogMAP(EntityTypeBuilder<MD.EventLog> entityBuilder)
        {
            #region Keys

            entityBuilder.HasKey(t => t.Id);

            #endregion

            #region Properties

            #endregion

            #region Table

            entityBuilder.ToTable("EventLogs");

            #endregion

        }
    }
}
