using System;
using System.Collections.Generic;
using System.Text;

using CT.Models.Base;

namespace CT.Models
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }

        public int OwnerID { get; set; }
        public Owner Owner { get; set; }

        public int PetTypeID { get; set; }
        public PetType PetType { get; set; }

    }
}
