using System;
using System.Collections.Generic;
using System.Text;

using CT.Models.Base;

namespace CT.Models
{
    public class PetType : BaseEntity
    {
        public string Type { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

    }
}
